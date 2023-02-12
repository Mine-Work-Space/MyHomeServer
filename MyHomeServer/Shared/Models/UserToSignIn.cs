using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeServer.Shared.Models
{
    public class UserToSignIn
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Пароль повинен бути довжиною між {2} та {1}", MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
