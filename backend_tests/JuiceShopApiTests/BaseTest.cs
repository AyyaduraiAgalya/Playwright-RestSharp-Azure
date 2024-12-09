using NUnit.Framework;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace JuiceShopApiTests.ApiTests
{
    public class BaseTest
    {
        protected RestClient _client;          // Shared RestSharp client
        protected IConfiguration _configuration; // Shared configuration
        protected string? _authToken = null;  // Nullable authentication token

        [SetUp]
        public void Setup()
        {
            // Load configuration from appsettings.json
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Initialize RestSharp client
            _client = new RestClient();

            // Authenticate and store the token
            _authToken = LoginAndGetToken();

            if (string.IsNullOrEmpty(_authToken))
            {
                throw new Exception("Authentication failed. Token is null or empty.");
            }
        }

        [TearDown]
        public void Cleanup()
        {
            _client?.Dispose();
        }

        protected string LoginAndGetToken()
        {
            // Retrieve credentials from configuration
            string baseUrl = _configuration["ApiBaseUrl"] ?? throw new Exception("Base URL is missing in appsettings.json");
            string email = _configuration["Credentials:Email"] ?? throw new Exception("Email is missing in appsettings.json");
            string password = _configuration["Credentials:Password"] ?? throw new Exception("Password is missing in appsettings.json");

            // Create a login request
            var request = new RestRequest($"{baseUrl}/rest/user/login", Method.Post);
            var requestBody = new { email, password };
            request.AddJsonBody(requestBody);

            // Execute the request
            var response = _client.Execute(request);

            if ((int)response.StatusCode != 200)
            {
                Console.WriteLine("Login request failed:");
                Console.WriteLine($"Status Code: {(int)response.StatusCode}");
                Console.WriteLine($"Response Body: {response.Content}");
                throw new Exception("Login API failed");
            }

            // Parse the JSON response to retrieve the token
            var jsonResponse = JObject.Parse(response.Content ?? throw new Exception("Response content is null"));
            var token = jsonResponse["authentication"]?["token"]?.ToString();

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Failed to retrieve authentication token");
            }

            // Return the authentication token
            return token;
        }
    }
}
