using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerKingBackEnd.Migrations
{
    public partial class columndAddedToCourierAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveringOrderId",
                table: "Courier",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveringOrderId",
                table: "Courier");
        }
    }
}
