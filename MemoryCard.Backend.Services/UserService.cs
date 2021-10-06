using MemoryCard.Backend.Models;
using MemoryCard.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Services
{
    public class UserService : IUserService
    {
        public bool AddUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public UserModel FindByGuid(string guid)
        {
            throw new NotImplementedException();
        }

        public UserModel FindByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
