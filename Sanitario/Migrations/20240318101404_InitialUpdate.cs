using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanitario.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Dipendenti",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Cognome",
                table: "Dipendenti",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Dipendenti",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Dipendenti",
                newName: "Cognome");
        }
    }
}
