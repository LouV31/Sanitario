using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanitario.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaForeignKeyAnimale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodiceFiscaleProprietario",
                table: "Animali");

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Animali",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animali_IdCliente",
                table: "Animali",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Animali_Clienti_IdCliente",
                table: "Animali",
                column: "IdCliente",
                principalTable: "Clienti",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animali_Clienti_IdCliente",
                table: "Animali");

            migrationBuilder.DropIndex(
                name: "IX_Animali_IdCliente",
                table: "Animali");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Animali");

            migrationBuilder.AddColumn<string>(
                name: "CodiceFiscaleProprietario",
                table: "Animali",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
