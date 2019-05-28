using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class AddConfirmedToUserTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                schema: "mvp",
                table: "UserTrip",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                schema: "mvp",
                table: "UserTrip");
        }
    }
}
