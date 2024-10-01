using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecotrack.Context
{
    public class EcotrackDbContext : DbContext
    {
        public EcotrackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcotrackDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}


