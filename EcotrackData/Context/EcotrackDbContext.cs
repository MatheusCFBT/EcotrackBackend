using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecotrack.Context
{
    public class EcotrackDbContext : DbContext
    {
        public EcotrackDbContext(DbContextOptions<EcotrackDbContext> options) : base(options)
        {
        }

        public class EcotrackDbContextFactory : IDesignTimeDbContextFactory<EcotrackDbContext>
        {
            public EcotrackDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EcotrackDbContext>();
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=@timao0508");

                return new EcotrackDbContext(optionsBuilder.Options);
            }
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcotrackDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}


