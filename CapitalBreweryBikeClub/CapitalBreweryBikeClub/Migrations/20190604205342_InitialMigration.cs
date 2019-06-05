using Microsoft.EntityFrameworkCore.Migrations;

namespace CapitalBreweryBikeClub.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RideWithGpsId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Available = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Mileage = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true)
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
