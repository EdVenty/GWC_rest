using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CreationDate { get; set; }
        public Message[] Messages { get; set; }
        public Image Avatar { get; set; }
        public User[] Members { get; set; }
    }
}
