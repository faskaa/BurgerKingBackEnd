using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerKingBackEnd.Migrations
{
    public partial class ChangedRestaurantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Restaurant");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CloseTime",
                table: "Restaurant",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpenTime",
                table: "Restaurant",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Restaurant");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Restaurant",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
