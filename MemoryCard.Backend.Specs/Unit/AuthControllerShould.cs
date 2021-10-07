using FluentAssertions;
using MemoryCard.Backend.Controllers;
using MemoryCard.Backend.Services;
using MemoryCard.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MemoryCard.Backend.Specs.Unit
{
    public class AuthControllerShould
    {
        Mock<IAuthService> _authService;

        public AuthControllerShould()
        {
            _authService = new Mock<IAuthService>();
            _authService.SetReturnsDefault("");

            _authService
                .Setup(service => service.AuthenticateAsync("nick", "1234"))
                .ReturnsAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IlJhbmRhbGwgRGVnZ2VzIiwiaWF0IjoxNTE2MjM5MDIyfQ.sNMELyC8ohN8WF_WRnRtdHMItOVizcscPiWsQJX9hmw");
        }

        [Theory]
        [InlineData("nick", "1234")]
        public async Task AuthenticateUser_WhenCredentialsAreCorrect(string username, string password)
        {
            var sut = new AuthController(_authService.Object);

            var response = await sut.Authenticate(username, password);

            response.Should().BeAssignableTo<OkObjectResult>("correct credentials should be accepted by the controller");
            response.As<OkObjectResult>().Value.ToString().Should().MatchRegex(MockUtils.JwtRegex, "the returned value should be a valid JSON Web Token");
        }

        [Theory]
        [InlineData("nick", "2345")]
        [InlineData("nack", "1234")]
        [InlineData("nack", "2345")]
        public async Task RejectAuthentication_WhenCredentialsAreWrong(string username, string password)
        {
            var sut = new AuthController(_authService.Object);

            var response = await sut.Authenticate(username, password);

            response.Should().BeAssignableTo<UnauthorizedResult>("incorrect credentials should be rejected by the controller");
        }

        [Theory]
        [InlineData("", "1234")]
        [InlineData("nick", "")]
        public async Task RejectAuthentication_WhenInputIsInvalid(string username, string password)
        {
            var sut = new AuthController(_authService.Object);

            var response = await sut.Authenticate(username, password);

            response.Should().BeAssignableTo<UnauthorizedResult>("lack of input should be rejected by the controller");
        }
    }
}
