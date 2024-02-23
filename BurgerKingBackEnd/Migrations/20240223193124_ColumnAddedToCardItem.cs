using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerKingBackEnd.Migrations
{
    public partial class ColumnAddedToCardItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "CardItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "CardItems");
        }
    }
}
