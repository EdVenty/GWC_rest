﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Image Preview { get; set; }
        [Required]
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public long CreationDate { get; set; }
    }
}
