﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCard.Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "")]
        public string Username { get; set; }

        [Required(ErrorMessage = "")]
        public string Password { get; set; }
    }
}
