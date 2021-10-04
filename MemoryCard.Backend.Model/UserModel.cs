using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Models
{
    class UserModel
    {
        [Key] public string Id { get; set; }
        public string Username { get; set; } /* Not used as key. Is unique, but may change in the future. */
        public string EmailAddress { get; set; } /* Idem, user may want to change email addresses in the future. */
        public string Password { get; set; }
        // TODO Reference to other objects


    }
}
