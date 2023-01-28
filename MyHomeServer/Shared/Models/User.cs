using System.ComponentModel.DataAnnotations;

namespace MyHomeServer.Shared.Models
{
    public class User
    {
        [Display(Name = "Ім'я")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Пошта")]
        public string? EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Пароль повинен бути довжиною між {2} та {1}", MinimumLength = 6)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }    
    }
}
