﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Token { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}