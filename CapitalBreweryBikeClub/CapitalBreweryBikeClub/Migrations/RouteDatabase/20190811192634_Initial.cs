using Microsoft.EntityFrameworkCore.Migrations;

namespace CapitalBreweryBikeClub.Migrations.RouteDatabase
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RideWithGpsId = table.Column<string>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Info = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Mileage = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RideWithGpsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
