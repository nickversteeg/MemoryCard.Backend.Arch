using FluentAssertions;
using MemoryCard.Backend.Controllers;
using MemoryCard.Backend.Services;
using MemoryCard.Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MemoryCard.Backend.Specs
{
    public class AuthenticationShould : IDisposable
    {
        public Mock<IAuthService> _authService = new Mock<IAuthService>();
        private const string CorrectJwt = "a1b2c3d4";

        public AuthenticationShould()
        {
            _authService.SetReturnsDefault("");
            _authService.Setup(u => u.Authenticate("nick", "1234")).Returns(CorrectJwt);

        }

        public void Dispose()
        {

        }

        [Fact]
        public void AuthenticateUserWithCorrectCredentials()
        {
            AuthController authController = new AuthController(_authService.Object);

            LoginViewModel loginViewModel = new LoginViewModel { Username = "nick", Password = "1234" };

            var response = authController.Authenticate(loginViewModel);

            response.Should().BeAssignableTo<OkObjectResult>("the API should handle the login request gracefully");
            response.As<OkObjectResult>().Value.Should().BeEquivalentTo(CorrectJwt, "the API should authenticate the user");

        }


        [Fact]
        public void RejectUserWithIncorrectCredentials()
        {
            AuthController authController = new AuthController(_authService.Object);

            LoginViewModel loginViewModel = new LoginViewModel { Username = "nack", Password = "2345" };

            var response = authController.Authenticate(loginViewModel);
            response.Should().BeAssignableTo<UnauthorizedResult>("the incorrect credentials should be rejected");
        }


    }
}
