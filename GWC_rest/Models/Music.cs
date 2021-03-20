using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public enum MusicType
    {
        Ogg, Mp3, Wav
    }
    public class Music
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public MusicType Type { get; set; }
        [Required]
        public string ItemId { get; set; }
        public string Title { get; set; }
        [Required]
        public long CreationDate { get; set; }
    }
}
