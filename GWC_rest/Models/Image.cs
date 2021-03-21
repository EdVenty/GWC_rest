using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual ICollection<ImageSize> Sizes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public virtual User Uploader { get; set; }
        [Required]
        public long CreationDate { get; set; }
    }
}
