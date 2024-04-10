﻿using System.ComponentModel.DataAnnotations;

namespace InsuranceCalc.Models.Authenticate
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
