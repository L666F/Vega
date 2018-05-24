using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models.Data
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {}

        public DbSet<Make> Makes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
    }
}
