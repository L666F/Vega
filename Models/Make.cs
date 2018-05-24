using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class Make
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public List<CarModel> Models { get; set; }

        public Make() => Models = new List<CarModel>();
    }
}
