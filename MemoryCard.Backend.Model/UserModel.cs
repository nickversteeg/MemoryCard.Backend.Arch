using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Models
{
    public class UserModel : IEntity
    {
        public string Guid { get; set; }
        public string EmailAddress { get; set; }
        public UserCredentialsModel Credentials { get; set; }
    }

}
