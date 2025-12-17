using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjesi.Migrations
{
    /// <inheritdoc />
    public partial class YorumEmailEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Yorumlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Yorumlar");
        }
    }
}
