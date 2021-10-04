using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Models
{
    class RegisterViewModel
    {
        [Required(ErrorMessage = "")]
        public string Username { get; set; }

        [Required(ErrorMessage = "")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "")]
        public string Password { get; set; }
    }
}
