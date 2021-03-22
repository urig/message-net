using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MessagesService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Shouldly;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration

namespace MessagesServiceTests
{
    [Trait("Category", "API Tests")]
    public class MessagesControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
      
        public MessagesControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Get_NoRecipient_Returns400()
        {
            // Act
            var response = await _client.GetAsync("/messages/");
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_NewRecipient_ReturnsEmptyList()
        {
            // ACt
            var response = await _client.GetAsync("/messages/foo");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.ShouldBe("[]");
        }

        [Fact]
        public async Task Post_NewRecipient_AddsMessage()
        {
            // Arrange
            var message = new
            {
                recipient = "bar",
                sender = "foo",
                content = "Hello, World!"
            };
            var json = JsonSerializer.Serialize<object>(message);
            // Act
            var response = await _client.PostAsync("/messages/bar", 
                new StringContent(json, Encoding.UTF8, "application/json"));
            // Assert
            response.EnsureSuccessStatusCode();
            await VerifyMessagesContain("/messages/bar", json);
        }

        private async Task VerifyMessagesContain(string requestUri, string json)
        {
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.ShouldContain(json);
        }
    }
}
