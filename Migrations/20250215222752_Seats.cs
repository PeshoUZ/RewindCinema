using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VintageCinema.Migrations
{
    /// <inheritdoc />
    public partial class Seats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReservedSeats",
                table: "Reservations",
                type: "json",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "JSON")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReservedSeats",
                table: "Reservations",
                type: "JSON",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "json")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
