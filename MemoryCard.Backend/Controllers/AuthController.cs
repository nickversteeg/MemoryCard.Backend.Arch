using MemoryCard.Backend.Models;
using MemoryCard.Backend.Services;
using MemoryCard.Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;

namespace MemoryCard.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(401)]
        public IActionResult Authenticate([FromBody] LoginViewModel login)
        {
            var token = _authService.Authenticate(login.Username, login.Password);
            return string.IsNullOrEmpty(token) ? Unauthorized() : Ok(token);
        }

    }
}
