using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class AddNullableForeignKeyAndFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Office_OfficeId",
                schema: "mvp",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRoom_Apartment_ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom");

            migrationBuilder.DropColumn(
                name: "AppartmentId",
                schema: "mvp",
                table: "ApartmentRoom");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                schema: "mvp",
                table: "Apartment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Office_OfficeId",
                schema: "mvp",
                table: "Apartment",
                column: "OfficeId",
                principalSchema: "mvp",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRoom_Apartment_ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom",
                column: "ApartmentId",
                principalSchema: "mvp",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Office_OfficeId",
                schema: "mvp",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRoom_Apartment_ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AppartmentId",
                schema: "mvp",
                table: "ApartmentRoom",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                schema: "mvp",
                table: "Apartment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Office_OfficeId",
                schema: "mvp",
                table: "Apartment",
                column: "OfficeId",
                principalSchema: "mvp",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRoom_Apartment_ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom",
                column: "ApartmentId",
                principalSchema: "mvp",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
