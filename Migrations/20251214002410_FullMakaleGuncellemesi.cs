using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjesi.Migrations
{
    /// <inheritdoc />
    public partial class FullMakaleGuncellemesi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "Makaleler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OkunmaSayisi",
                table: "Makaleler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "OneCikarilsinMi",
                table: "Makaleler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TahminiSure",
                table: "Makaleler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "Makaleler");

            migrationBuilder.DropColumn(
                name: "OkunmaSayisi",
                table: "Makaleler");

            migrationBuilder.DropColumn(
                name: "OneCikarilsinMi",
                table: "Makaleler");

            migrationBuilder.DropColumn(
                name: "TahminiSure",
                table: "Makaleler");
        }
    }
}
