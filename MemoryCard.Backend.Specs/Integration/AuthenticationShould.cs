using FluentAssertions;
using MemoryCard.Backend.Controllers;
using MemoryCard.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MemoryCard.Backend.Specs.Integration
{
    public class AuthenticationShould : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        readonly WebApplicationFactory<Startup> _factory;


        public AuthenticationShould(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;

        }

        public void Dispose()
        {
            _factory.Dispose();
        }

        [Theory]
        [InlineData("api/login", "nick", "1234")]
        public async Task AuthenticateUserWithCorrectCredentials(string url, string username, string password)
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync(url, JsonContent.Create((username, password)));

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, "the request should be accepted by the API");

            response.Content.Should().BeOfType<string>().And.NotBe("", "the response should contain a JWT");


        }


        [Theory]
        [InlineData("api/login", "nack", "2345")]
        public async Task RejectUserWithIncorrectCredentials(string url, string username, string password)
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync(url, JsonContent.Create((username, password)));

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized, "the request should reject incorrect ");


        }

    }
}
