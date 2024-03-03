using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerKingBackEnd.Migrations
{
    public partial class ColumnAddedToCourier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelivering",
                table: "Courier",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelivering",
                table: "Courier");
        }
    }
}
