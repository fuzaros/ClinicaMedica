using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaMedica.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoEspecialidadeNaConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Especialidade",
                table: "Consulta",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especialidade",
                table: "Consulta");
        }
    }
}
