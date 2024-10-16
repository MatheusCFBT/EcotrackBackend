using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcotrackData.Migrations
{
    /// <inheritdoc />
    public partial class ecotrackV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Clientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Clientes",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "");
        }
    }
}
