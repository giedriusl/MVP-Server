using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVP.DataAccess.Migrations
{
    public partial class mvpInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mvp");

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(maxLength: 500, nullable: true),
                    CountryCode = table.Column<string>(maxLength: 500, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    LocationId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Office_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "mvp",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Office_Location_LocationId1",
                        column: x => x.LocationId1,
                        principalSchema: "mvp",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    BedCount = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "mvp",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apartment_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalSchema: "mvp",
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    FromOfficeId = table.Column<int>(nullable: false),
                    ToOfficeId = table.Column<int>(nullable: false),
                    TripStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Office_FromOfficeId",
                        column: x => x.FromOfficeId,
                        principalSchema: "mvp",
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Office_ToOfficeId",
                        column: x => x.ToOfficeId,
                        principalSchema: "mvp",
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentRoom",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    RoomNumber = table.Column<int>(nullable: false),
                    AppartmentId = table.Column<int>(nullable: false),
                    BedCount = table.Column<int>(nullable: false),
                    ApartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentRoom_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalSchema: "mvp",
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightInformation",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripId = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightInformation_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "mvp",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalCarInformation",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripId = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalCarInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalCarInformation_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "mvp",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "mvp",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                schema: "mvp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    ApartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendar_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalSchema: "mvp",
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calendar_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "mvp",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_LocationId",
                schema: "mvp",
                table: "Apartment",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_OfficeId",
                schema: "mvp",
                table: "Apartment",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRoom_ApartmentId",
                schema: "mvp",
                table: "ApartmentRoom",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_ApartmentId",
                schema: "mvp",
                table: "Calendar",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_UserId1",
                schema: "mvp",
                table: "Calendar",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FlightInformation_TripId",
                schema: "mvp",
                table: "FlightInformation",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_LocationId",
                schema: "mvp",
                table: "Office",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_LocationId1",
                schema: "mvp",
                table: "Office",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_RentalCarInformation_TripId",
                schema: "mvp",
                table: "RentalCarInformation",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_FromOfficeId",
                schema: "mvp",
                table: "Trip",
                column: "FromOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_ToOfficeId",
                schema: "mvp",
                table: "Trip",
                column: "ToOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TripId",
                schema: "mvp",
                table: "User",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentRoom",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "Calendar",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "FlightInformation",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "RentalCarInformation",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "Apartment",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "User",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "Trip",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "Office",
                schema: "mvp");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "mvp");
        }
    }
}
