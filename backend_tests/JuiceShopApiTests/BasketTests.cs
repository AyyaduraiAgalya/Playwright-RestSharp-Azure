using NUnit.Framework;
using RestSharp;
using System;

namespace JuiceShopApiTests.ApiTests
{
    [TestFixture]
    public class BasketTests : BaseTest
    {
        [Test]
        public void TestGetBasketApi()
        {
            // The token is already retrieved during Setup() in BaseTest
            Assert.That(_authToken, Is.Not.Null.And.Not.Empty, "Authentication token is null or empty");

            // Create the request for the Basket API
            string baseUrl = _configuration["ApiBaseUrl"] ?? throw new Exception("Base URL is missing in appsettings.json");
            var request = new RestRequest($"{baseUrl}/rest/basket/1", Method.Get);
            request.AddHeader("Authorization", $"Bearer {_authToken}");

            // Log request details
            Console.WriteLine("=== REQUEST ===");
            Console.WriteLine($"URL: {request.Resource}");
            Console.WriteLine($"Headers: Authorization: Bearer <hidden>");

            // Execute the request and get the response
            var response = _client.Execute(request);

            // Log response details
            Console.WriteLine("=== RESPONSE ===");
            Console.WriteLine($"Status Code: {(int)response.StatusCode}");
            Console.WriteLine($"Body: {response.Content}");

            // Assert that the response status is 200 (OK)
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Basket API did not return status 200");

            // Assert the response contains expected basket details
            Assert.That(response.Content, Does.Contain("data"), "Basket API response does not contain expected content");
        }
    }
}
