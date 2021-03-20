using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public class Audio
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemId { get; set; }
        public float Duration { get; set; }
        [Required]
        public long CreationDate { get; set; }
    }
}
