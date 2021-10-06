using MemoryCard.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Services.Interfaces
{
    public interface IUserService
    {
        UserModel FindByUsernameAsync(string username);

        UserModel FindByGuid(string guid);

        bool AddUser(UserModel model);
    }
}
