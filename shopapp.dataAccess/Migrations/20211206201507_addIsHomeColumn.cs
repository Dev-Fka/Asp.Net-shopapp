using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopapp.dataAccess.Migrations
{
    public partial class addIsHomeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ısHome",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ısHome",
                table: "Products");
        }
    }
}
