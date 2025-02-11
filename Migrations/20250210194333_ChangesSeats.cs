using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VintageCinema.Migrations
{
    /// <inheritdoc />
    public partial class ChangesSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Screenings",
                newName: "DateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Screenings",
                newName: "dateTime");
        }
    }
}
