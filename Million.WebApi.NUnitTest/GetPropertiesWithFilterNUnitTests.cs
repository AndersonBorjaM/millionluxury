using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Million.WebApplication.Test
{
    [TestFixture]
    public class GetPropertiesWithFilterNUnitTests
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GetAuthTokenAsync().Result}");
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        private async Task<string> GetAuthTokenAsync()
        {
            var requestBody = new
            {
                userName = "Anderson",
                password = "string"
            };

            var response = await _client.PostAsJsonAsync("/api/User/Login", requestBody);
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadAsStringAsync();

            return tokenResponse ?? throw new Exception("No se pudo obtener el token.");
        }

        [Test]
        public async Task GetPropertiesWithFilter_ValidRequest_ReturnListProperties()
        {

            // Arrange
            var requestBody = new StringContent(JsonSerializer.Serialize(new
            {
                name = "Dora",
                address = "",
                year = "",
                codeInternal = "",
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Property/GetPropertiesWithFilter", requestBody);

            // Assert
            Assert.That(HttpStatusCode.OK.Equals(response.StatusCode));
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.That(responseContent.Contains("Dora"));
        }



    }
}
