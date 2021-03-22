using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MessagesService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            var message = new Message
            {
                Recipient = "bar",
                Sender = "foo",
                Content = "Hello, World!"
            };
            var json = JsonSerializer.Serialize<object>(message);
            // Act
            var response = await _client.PostAsync("/messages/bar", 
                new StringContent(json, Encoding.UTF8, "application/json"));
            // Assert
            response.EnsureSuccessStatusCode();
            await VerifyActualMessage("/messages/bar", message);
        }

        private async Task VerifyActualMessage(string requestUri, Message expected)
        {
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var messages = JsonConvert.DeserializeObject<List<Message>>(responseString);
            var actual = messages.Single();
            actual.Recipient.ShouldBe(expected.Recipient);
            actual.Sender.ShouldBe(expected.Sender);
            actual.Content.ShouldBe(expected.Content);
        }
    }
}
