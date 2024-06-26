﻿using System.ComponentModel.DataAnnotations;

namespace MyHomeServer.Shared.Models
{
    public class UserDTO
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Пароль повинен бути довжиною між {2} та {1}", MinimumLength = 6)]
        public string? Password { get; set; }    
    }
}
