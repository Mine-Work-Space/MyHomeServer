using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace MyHomeServer.Server.Data.DbModels
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2048, ErrorMessage = "Повідомлення повинне бути довжиною між {2} та {1}", MinimumLength = 1)]
        [Display(Name = "Повідомлення")]
        public string Content { get; set; } = string.Empty;
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; } = string.Empty;
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual IEnumerable<Group>? Groups { get; set; }
    }
}
