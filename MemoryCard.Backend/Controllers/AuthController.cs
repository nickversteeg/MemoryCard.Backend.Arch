using MemoryCard.Backend.Models;
using MemoryCard.Backend.Services;
using MemoryCard.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Threading.Tasks;

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
        [Route("api/login")]
        public async Task<IActionResult> Authenticate([FromBody] string username, [FromBody] string password)
        {
            var token = await _authService.AuthenticateAsync(username, password);
            return string.IsNullOrEmpty(token) ? Unauthorized() : Ok(token);
        }

    }
}
