using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeServer.Shared.Models
{
    public class GroupDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
