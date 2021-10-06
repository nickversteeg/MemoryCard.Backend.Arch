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
        public IEnumerable<UserModel> Get()
        {
            return new List<UserModel>();
        }

        [HttpGet("{id}")]
        public UserModel Get(string guid)
        {
            return _userService.FindByGuid(guid);
        }

        [HttpPost]
        public ActionResult Create(RegisterViewModel model) // api/user/create
        {
            var user = new UserModel
            {
                Guid = Guid.NewGuid().ToString(),
                EmailAddress = model.EmailAddress,
                Credentials = new UserCredentialsModel
                {
                    Username = model.Username,
                    Password = model.Password
                }
            };

            var result = _userService.AddUser(user);

            if (!result) return StatusCode(500); // TODO Better handling of errors
            return StatusCode(201); // Inserted
        }
    }
}
