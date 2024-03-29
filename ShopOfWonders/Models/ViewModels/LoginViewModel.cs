﻿using System.ComponentModel.DataAnnotations;

namespace SOW.ShopOfWonders.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
