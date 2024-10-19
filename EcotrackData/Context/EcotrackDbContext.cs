using EcotrackBusiness.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecotrack.Context
{
    public class EcotrackDbContext : IdentityDbContext
    {
        public EcotrackDbContext(DbContextOptions<EcotrackDbContext> options) : base(options)
        {
        }

        public class EcotrackDbContextFactory : IDesignTimeDbContextFactory<EcotrackDbContext>
        {
            // O metodo CreateDbContext cria e retorna uma instância do EcotrackDbContext configurada para usar o PostgreSQL.
            public EcotrackDbContext CreateDbContext(string[] args)
            {
                // Cria um novo DbContextOptionsBuilder para o contexto EcotrackDbContext e configura a conexão com o DB
                var optionsBuilder = new DbContextOptionsBuilder<EcotrackDbContext>();
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=@timao0508");

                return new EcotrackDbContext(optionsBuilder.Options);
            }
        }

        // Define um DbSet para a entidade Cliente, que representa uma tabela no banco de dados. Permite realizar operações CRUD (Create, Read, Update, Delete) no banco para essa entidade
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Carrega automaticamente todas as configurações de entidades encontradas no assembly da classe EcotrackDbContext.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcotrackDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}


