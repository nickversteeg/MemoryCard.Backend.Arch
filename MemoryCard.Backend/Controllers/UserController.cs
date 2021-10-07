using MemoryCard.Backend.Models;
using MemoryCard.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserModel> Get() // api/user
        {
            return new List<UserModel>();
        }

        [HttpGet("{id}")]
        public UserModel Get(string guid) // api/user/{id}
        {
            return _userService.FindByGuid(guid);
        }

        [HttpPost]
        public ActionResult Create([FromBody] string username, [FromBody] string emailAddress, [FromBody] string password) // api/user/create
        {
            var user = new UserModel
            {
                Guid = Guid.NewGuid().ToString(),
                EmailAddress = emailAddress,
                Credentials = new UserCredentialsModel
                {
                    Username = username,
                    Password = password
                }
            };

            var result = _userService.AddUser(user);

            if (!result) return StatusCode(500); // TODO Better handling of errors
            return StatusCode(201); // Inserted
        }
    }
}
