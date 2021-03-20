using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        [Required]
        public int CreationDate { get; set; }
    }
}
