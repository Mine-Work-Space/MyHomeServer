using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeServer.Shared.Models
{
    public class MessageDTO
    {
        public string Content { get; set; }
        public string SenderUser { get; set; }
        public string ChatId { get; set; }
        public DateTime SendDate { get; set; }
    }
}
