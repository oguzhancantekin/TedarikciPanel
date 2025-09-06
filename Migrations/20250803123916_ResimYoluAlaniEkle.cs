using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TedarikciPanel.Migrations
{
    /// <inheritdoc />
    public partial class ResimYoluAlaniEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResimYolu",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResimYolu",
                table: "Products");
        }
    }
}
