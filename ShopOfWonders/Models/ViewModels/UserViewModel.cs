﻿using SOW.DataModels;
using System.ComponentModel.DataAnnotations;

namespace SOW.ShopOfWonders.Models.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        public string Login { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Поле является обязательным")]
        public string Email { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronomic { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
        
        public byte[]? ProfilePicture { get; set; }

    }
}
