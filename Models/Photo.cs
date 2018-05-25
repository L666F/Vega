using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class Photo
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}
