using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjesi.Migrations
{
    /// <inheritdoc />
    public partial class YazarEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Makaleler",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Makaleler_UserId",
                table: "Makaleler",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Makaleler_AspNetUsers_UserId",
                table: "Makaleler",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Makaleler_AspNetUsers_UserId",
                table: "Makaleler");

            migrationBuilder.DropIndex(
                name: "IX_Makaleler_UserId",
                table: "Makaleler");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Makaleler");
        }
    }
}
