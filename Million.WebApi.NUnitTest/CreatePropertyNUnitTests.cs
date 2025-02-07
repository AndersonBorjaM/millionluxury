using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework.Internal;

namespace Million.WebApplication.Test
{
    [TestFixture]
    public class CreatePropertyNUnitTests
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

        [Test]
        public async Task CreateOwnerAndProperty_ReturnsCreatedResponse_WhenValidRequest()
        {
            // Arrange
            var responseOwner = await _client.PostAsync("/api/Owner/CreateOwner", new MultipartFormDataContent
            {
                { new StringContent("Carmen"), "name" },
                { new StringContent("Calle 16 Bello"), "address" },
                { new StringContent("1997-06-08"), "birthday" },
            });

            var responseContent = await responseOwner.Content.ReadAsStringAsync();
            var infoOwner = JsonSerializer.Deserialize<OwnerResponse>(responseContent);

            var requestPropertyBody = new StringContent(JsonSerializer.Serialize(new
            {
                name = "Test Property",
                address = "123 Test Address",
                price = 200000.62,
                codeInternal = "C54512",
                year = "2025",
                idOwner = infoOwner!.idOwner,
                propertyTraces = new List<object>(),

            }), Encoding.UTF8, "application/json");


            // Act
            var response = await _client.PostAsync("/api/Property/CreateProperty", requestPropertyBody);

            // Assert
            Assert.That(HttpStatusCode.OK.Equals(response.StatusCode));

            var responsePropertyContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responsePropertyContent, "El contenido de la respuesta no debería estar vacío.");
        }


        [Test]
        [TestCaseSource(nameof(GetValidPropertyTestCases))]
        public async Task CreateProperty_ReturnsCreatedResponse_WhenValidRequest(object property, string expectedErrorMessage)
        {
            // Arrange
            var requestBody = new StringContent(JsonSerializer.Serialize(property), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Property/CreateProperty", requestBody);

            // Assert
            Assert.That(HttpStatusCode.OK.Equals(response.StatusCode));

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains(expectedErrorMessage));
        }

        [Test]
        [TestCaseSource(nameof(GetInvalidPropertyTestCases))]
        public async Task CreateProperty_ReturnsBadRequest_WhenInvalidRequest(object property, string expectedErrorMessage)
        {
            // Arrange
            var requestBody = new StringContent(JsonSerializer.Serialize(property), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Property/CreateProperty", requestBody);

            // Assert
            Assert.That(HttpStatusCode.BadRequest.Equals(response.StatusCode));

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains(expectedErrorMessage));
        }


        private static IEnumerable<object[]> GetValidPropertyTestCases()
        {
            yield return new object[] { new {
                name = "Anderson test",
                address = "123 Test Address",
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
            }, "Anderson test" };

            yield return new object[] { new {
                name = "Camilo test",
                address = "Diagonal 25 calle 65",
                price = 9657522.42,
                codeInternal = "A1852",
                year = "2000",
                propertyTraces = new[]
                {
                    new
                    {
                        dateSale = "2020-11-25",
                        name = "Trace Camilo",
                        value = 35548784.52,
                        tax = "10%",
                        idProperty = 0
                    }
                },
                owner = new
                {
                    name = "Catalina",
                    address = "456 635",
                    birthday = "1900-01-01"
                }
            }, "Diagonal 25 calle 65" };


            yield return new object[] { new {
                name = "Dora",
                address = "Carrera 586",
                price = 6352000.25,
                codeInternal = "B58744",
                year = "2019",
                propertyTraces = new List<object>(),
                owner = new
                {
                    name = "Clara",
                    address = "Transversal 5",
                    birthday = "1980-11-21"
                }
            }, "B58744" };
        }



        private static IEnumerable<object[]> GetInvalidPropertyTestCases()
        {
            yield return new object[] { new {
                name = "", // Nombre inválido
                address = "123 Test Address",
                price = 25365.00,
                codeInternal = "A123",
                year = "2025",
                idOwner = 0,
                owner = new
                {
                    name = "John Doe",
                    address = "456 Owner Address",
                    birthday = "1990-01-01"
                }
            }, "The field Name is required." };


            yield return new object[] { new {
                name = "Anderson test",
                address = "",//Dirección invalida
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
            }, "The field Address is required." };

            yield return new object[] { new {
                name = "Price",
                address = "123 Test Address",
                price = 999999999999.252,//Precio invalido
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
            }, "The price cannot have a value greater than 999999999 and cannot have more than two decimal places." };


            yield return new object[] { new {
                name = "CodeInternal",
                address = "123 Test Address",
                price = 152336.58,
                codeInternal = "",//Codigo interno incorrecto.
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
            }, "The field Code Internal is required." };


            yield return new object[] { new {
                name = "CodeInternal",
                address = "123 Test Address",
                price = 152336.58,
                codeInternal = "jnsdj57",
                year = "2025000", // año invalido
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
            }, "The maximum length of the Year field is 4." };


            yield return new object[] { new {
                name = "Owner",
                address = "123 Test Address",
                price = 152336.58,
                codeInternal = "jnsdj57",
                year = "2025",
                idOwner = 0, // Propietario invalido
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
                }// Propietario invalido
            }, "The information from Owner is required." };

            yield return new object[] { new {
                name = "Owner",
                address = "123 Test Address",
                price = 152336.58,
                codeInternal = "jnsdj57",
                year = "2025",
                idOwner = 0,
                propertyTraces = new[]
                {
                    new
                    {
                        //Fecha invalida
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
            }, "The field DateSale is required." };


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

        private async Task<string> CreateOwner()
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

    }

    public class OwnerResponse 
    {
        public int idOwner { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }
    }

}
