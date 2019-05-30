using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class AddOrganizerToTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizerId",
                schema: "mvp",
                table: "Trip",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_OrganizerId",
                schema: "mvp",
                table: "Trip",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_AspNetUsers_OrganizerId",
                schema: "mvp",
                table: "Trip",
                column: "OrganizerId",
                principalSchema: "mvp",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_AspNetUsers_OrganizerId",
                schema: "mvp",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_OrganizerId",
                schema: "mvp",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                schema: "mvp",
                table: "Trip");
        }
    }
}
