using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Million.WebApplication.Test
{
    [TestFixture]
    public class CreateOwnerNUnitTests
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
        public async Task CreateOwner_WithRealImageFile_ReturnsSuccess()
        {
            // Arrange
            using var httpClient = new HttpClient();
            var imageStream = await httpClient.GetStreamAsync("https://picsum.photos/200/300");

            
            var formData = new MultipartFormDataContent
            {
                { new StringContent("Juan Carlos"), "name" },
                { new StringContent("Medellin"), "address" },
                { new StringContent("2000-12-02"), "birthday" },
                { new StreamContent(imageStream), "photo", "photo.jpg" }
            };

            // Act
            var response = await _client.PostAsync("/api/Owner/CreateOwner", formData);

            // Assert
            Assert.That(HttpStatusCode.OK.Equals(response.StatusCode));
        }

        [Test]
        public async Task CreateOwner_WithoutImage_ReturnsSuccess()
        {
            // Arrange
            var formData = new MultipartFormDataContent
            {
                { new StringContent("John Doe"), "name" },
                { new StringContent("123 Main St"), "address" },
                { new StringContent("2000-01-01"), "birthday" }
            };

            // Act
            var response = await _client.PostAsync("/api/Owner/CreateOwner", formData);

            // Assert
            Assert.That(HttpStatusCode.OK.Equals(response.StatusCode));
        }

    }



}
