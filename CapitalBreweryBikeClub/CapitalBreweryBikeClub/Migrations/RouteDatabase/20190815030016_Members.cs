using Microsoft.EntityFrameworkCore.Migrations;

namespace CapitalBreweryBikeClub.Migrations.RouteDatabase
{
    public partial class Members : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "Members",
                column: "Email",
                value: "mike.swart@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
