using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class UpdateRentalCarInfoFlightInfoEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DropOffAddress",
                schema: "mvp",
                table: "RentalCarInformation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickupAddress",
                schema: "mvp",
                table: "RentalCarInformation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromAirport",
                schema: "mvp",
                table: "FlightInformation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToAirport",
                schema: "mvp",
                table: "FlightInformation",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffAddress",
                schema: "mvp",
                table: "RentalCarInformation");

            migrationBuilder.DropColumn(
                name: "PickupAddress",
                schema: "mvp",
                table: "RentalCarInformation");

            migrationBuilder.DropColumn(
                name: "FromAirport",
                schema: "mvp",
                table: "FlightInformation");

            migrationBuilder.DropColumn(
                name: "ToAirport",
                schema: "mvp",
                table: "FlightInformation");
        }
    }
}
