using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CazuelaChapina.API.Migrations
{
    public partial class AddComboSeasonAndEditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEditable",
                table: "Combos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Combos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEditable",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Combos");
        }
    }
}
