using NUnit.Framework;

namespace JuiceShopApiTests.ApiTests
{
    [TestFixture]
    public class AuthTests : BaseTest
    {
        [Test]
        public void TestLoginApi()
        {
            // Login and retrieve the token
            _authToken = LoginAndGetToken();

            // Assert that the token is valid
            Assert.That(_authToken, Is.Not.Null.And.Not.Empty, "Authentication token is null or empty");

            // Log the token
            Console.WriteLine("=== AUTH TOKEN ===");
            Console.WriteLine($"Token: {_authToken}");
        }
    }
}
