using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Services
{
    public interface IAuthService
    {
        bool Authenticate(dynamic data);
    }
}
