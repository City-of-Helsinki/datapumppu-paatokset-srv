using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using AhjoApiService.AhjoApi;

namespace AhjoApiServiceUnitTests.AhjoApi
{
    public class AhjoApiConnectionTests
    {
        [Fact]
        public void CreateConnection_ReturnsHttpClientWithCorrectBaseAddressAndHeader()
        {
            // Arrange
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["AHJO_API_URL"]).Returns("https://api.test.com/");
            mockConfig.Setup(c => c["AHJO_API_KEY"]).Returns("test-api-key");

            var connection = new AhjoApiConnection(mockConfig.Object);

            // Act
            var httpClient = connection.CreateConnection();

            // Assert
            Assert.Equal("https://api.test.com/", httpClient.BaseAddress?.ToString());
            Assert.True(httpClient.DefaultRequestHeaders.Contains("api-key"));
            Assert.Equal("test-api-key", httpClient.DefaultRequestHeaders.GetValues("api-key").First());
        }
    }
}
