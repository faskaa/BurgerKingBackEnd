using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerKingBackEnd.Migrations
{
    public partial class IsSubmitedColumnAddedToCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmited",
                table: "CardItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmited",
                table: "CardItems");
        }
    }
}
