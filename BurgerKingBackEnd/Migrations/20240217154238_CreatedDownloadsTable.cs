using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerKingBackEnd.Migrations
{
    public partial class CreatedDownloadsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GooglePlayImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppStoreImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayStoreUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppStoreUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Downloads");
        }
    }
}
