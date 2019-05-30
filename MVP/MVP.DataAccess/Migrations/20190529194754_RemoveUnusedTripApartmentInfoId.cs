using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class RemoveUnusedTripApartmentInfoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripApartmentInfoId",
                schema: "mvp",
                table: "Calendar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripApartmentInfoId",
                schema: "mvp",
                table: "Calendar",
                nullable: true);
        }
    }
}
