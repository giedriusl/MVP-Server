using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class AddCascadeDeleteOnRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_ApartmentRoom_ApartmentRoomId",
                schema: "mvp",
                table: "Calendar");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_ApartmentRoom_ApartmentRoomId",
                schema: "mvp",
                table: "Calendar",
                column: "ApartmentRoomId",
                principalSchema: "mvp",
                principalTable: "ApartmentRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_ApartmentRoom_ApartmentRoomId",
                schema: "mvp",
                table: "Calendar");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_ApartmentRoom_ApartmentRoomId",
                schema: "mvp",
                table: "Calendar",
                column: "ApartmentRoomId",
                principalSchema: "mvp",
                principalTable: "ApartmentRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
