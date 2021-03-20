using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public enum AttachmentType
    {
       Photo, Video, Audio, Music, File
    }
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public AttachmentType Type { get; set; }
        public Image Photo { get; set; }
        public Video Video { get; set; }
        public Audio Audio { get; set; }
        public Music Music { get; set; }
        public File File { get; set; }
        [Required]
        public int CreationDate { get; set; }
    }
}
