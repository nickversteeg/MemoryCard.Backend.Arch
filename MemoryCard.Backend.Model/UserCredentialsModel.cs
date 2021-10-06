using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Models
{
    public class UserCredentialsModel : IEntity
    {
        public string Guid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
