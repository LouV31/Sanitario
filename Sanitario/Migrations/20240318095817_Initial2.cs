using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanitario.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInizioRicover",
                table: "AnimaliSmarriti",
                newName: "DataInizioRicovero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInizioRicovero",
                table: "AnimaliSmarriti",
                newName: "DataInizioRicover");
        }
    }
}
