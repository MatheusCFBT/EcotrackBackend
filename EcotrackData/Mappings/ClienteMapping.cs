using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcotrackData.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(70)");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(14)");
            
            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Clientes");
        }
    }
}