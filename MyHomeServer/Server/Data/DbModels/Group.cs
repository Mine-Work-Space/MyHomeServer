using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyHomeServer.Server.Data.DbModels
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Назва групи")]
        [StringLength(100, ErrorMessage = "Пароль повинен бути довжиною між {2} та {1}", MinimumLength = 1)]
        public string? Name { get; set; }
        public virtual IEnumerable<ApplicationUser> Users { get; set; }
    }
}
