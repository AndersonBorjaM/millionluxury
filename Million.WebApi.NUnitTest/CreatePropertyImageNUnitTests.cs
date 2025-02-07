using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Million.WebApplication.Test
{
    [TestFixture]
    public class CreatePropertyImageNUnitTests
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
        public async Task CreatePropertyImage_WithRealImageFile_ReturnsSuccess()
        {
            // Arrange
            using var httpClient = new HttpClient();
            var imageStream = await httpClient.GetStreamAsync("https://picsum.photos/200/300");


            var requestPropertyBody = new StringContent(JsonSerializer.Serialize(new
            {
                name = "Anderson test Image",
                address = "123 Image Test Address",
                price = 6352.25,
                codeInternal = "A123",
                year = "2025",
                idOwner = 0,
                propertyTraces = new[]
                {
                    new
                    {
                        dateSale = "2025-01-25",
                        name = "Trace 1",
                        value = 35264.52,
                        tax = "12%",
                        idProperty = 0
                    }
                },
                owner = new
                {
                    name = "John Doe",
                    address = "456 Owner Address",
                    birthday = "1990-01-01"
                }
            }), Encoding.UTF8, "application/json");

            var responseProperty = await _client.PostAsync("/api/Property/CreateProperty", requestPropertyBody);

            var responseContent = await responseProperty.Content.ReadAsStringAsync();
            var infoProperty = JsonSerializer.Deserialize<PropertyResponse>(responseContent);



            var formData = new MultipartFormDataContent
            {
                { new StringContent(infoProperty!.idProperty.ToString()), "idProperty" },
                { new StringContent("true"), "enabled" },
                { new StreamContent(imageStream), "fileProperty", "photo.jpg" }
            };

            // Act
            var response = await _client.PostAsync("/api/PropertyImage/CreatePropertyImage", formData);

            // Assert
            Assert.That(HttpStatusCode.OK.Equals(response.StatusCode));
        }

        [Test]
        public async Task CreatePropertyImage_WithoutImage_ReturnInvalidRequest()
        {

            var formData = new MultipartFormDataContent
            {
                { new StringContent("10"), "idProperty" },
                { new StringContent("true"), "enabled" },
            };

            // Act
            var response = await _client.PostAsync("/api/PropertyImage/CreatePropertyImage", formData);

            // Assert
            Assert.That(HttpStatusCode.BadRequest.Equals(response.StatusCode));
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("The field File is required."));
        }


    }

    public class PropertyResponse
    {
        public int idProperty { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public decimal price { get; set; }
        public string? codeInternal { get; set; }
        public string? year { get; set; }
        public int idOwner { get; set; }
    }

}
