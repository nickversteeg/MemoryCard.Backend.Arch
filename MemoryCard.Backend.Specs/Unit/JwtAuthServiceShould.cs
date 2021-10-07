using FluentAssertions;
using MemoryCard.Backend.Models;
using MemoryCard.Backend.Repositories;
using MemoryCard.Backend.Services;
using MemoryCard.Backend.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MemoryCard.Backend.Specs.Unit
{
    public class JwtAuthServiceShould
    {
        IRepository<UserCredentialsModel> _userCredentialsRepository;

        public JwtAuthServiceShould()
        {
            var mock = new Mock<IRepository<UserCredentialsModel>>();

            var users = new[] {
                new UserCredentialsModel { Guid = "e6d97157-1eac-4568-9cdc-a2a60d3bff02", Username = "nick", Password = "1234" },
                new UserCredentialsModel { Guid = "825e9b1d-1543-4014-84e0-eb79bcc73ae4", Username = "nock", Password = "2345" },
                new UserCredentialsModel { Guid = "4009cf31-0473-42e6-b306-cdc0bc76fe98", Username = "neck", Password = "3456" },
            };

            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).Throws<ArgumentException>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsRegex(MockUtils.JwtRegex))).ReturnsAsync((UserCredentialsModel) null);
            mock.Setup(repo => repo.GetByIdAsync("e6d97157-1eac-4568-9cdc-a2a60d3bff02")).ReturnsAsync(users[0]);

            mock.Setup(repo => repo.FindFirstAsync(It.IsAny<Func<UserCredentialsModel, bool>>())).ReturnsAsync(users[1]);

            mock.Setup(repo => repo.FindRangeAsync(It.IsAny<Func<UserCredentialsModel, bool>>())).ReturnsAsync(users[..^1]);

            _userCredentialsRepository = mock.Object;
        }

        [Theory]
        [InlineData("nick", "1234")]
        public async Task AuthenticateUser_WithCorrectCredentials(string username, string password)
        {
            JwtAuthService sut = new JwtAuthService(_userCredentialsRepository);

            var result = await sut.AuthenticateAsync(username, password);

            result.Should().MatchRegex(MockUtils.JwtRegex, "correct authentication should authenticate the user and give them a JSON Web Token");
            
        }

        [Theory]
        [InlineData("nick", "2345")]
        [InlineData("nack", "1234")]
        public async Task RejectAuthentication_WhenCredentialsAreWrong(string username, string password)
        {
            JwtAuthService sut = new JwtAuthService(_userCredentialsRepository);

            var result = await sut.AuthenticateAsync(username, password);

            result.Should().BeEmpty("incorrect authentication should not give the user a JSON Web Token");
        }

        [Theory]
        [InlineData("", "1234")]
        [InlineData("nick", "")]
        public async Task RejectAuthentication_WhenInputIsInvalid(string username, string password)
        {
            JwtAuthService sut = new JwtAuthService(_userCredentialsRepository);

            var result = await sut.AuthenticateAsync(username, password);

            result.Should().BeEmpty("improper authentication parameters should not give the user a JSON Web Token");
        }
    }
}
