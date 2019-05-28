using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class RemoveApartmentRoomIdConstraintFromCalendarEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_ApartmentRoom_ApartmentRoomId",
                schema: "mvp",
                table: "Calendar");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentRoomId",
                schema: "mvp",
                table: "Calendar",
                nullable: true,
                oldClrType: typeof(int));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_ApartmentRoom_ApartmentRoomId",
                schema: "mvp",
                table: "Calendar");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentRoomId",
                schema: "mvp",
                table: "Calendar",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
