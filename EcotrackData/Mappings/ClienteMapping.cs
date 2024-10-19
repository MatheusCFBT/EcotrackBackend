using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcotrackData.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        // Mapenado a entidade Cliente no ORM e para o Banco de dados
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Mapenado o Id
            builder.HasKey(c => c.Id);

            // Mapeando o Nome, adicionando o atributo "requirido" e com o maximo de 70 caracteres
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(70)");

            // Mapeando o Cpf, adicionando o atributo "requirido" e com o maximo de 70 caracteres
            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(14)");
            
            // Mapeando o Email, adicionando o atributo "requirido" e com o maximo de 100 caracteres
            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // Criando a tabela Clientes
            builder.ToTable("Clientes");
        }
    }
}