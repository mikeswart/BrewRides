using Microsoft.EntityFrameworkCore.Migrations;

namespace CapitalBreweryBikeClub.Migrations
{
    public partial class MemberInfoAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Members",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Members");
        }
    }
}
