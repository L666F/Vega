using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using vega.Models;

namespace vega.Controllers.Resources
{
    public class MakeResource
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<CarModelResource> Models { get; set; }

        public MakeResource() => Models = new List<CarModelResource>();
    }
}
