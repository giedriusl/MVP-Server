﻿// <auto-generated />
using System;
using MVP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVP.DataAccess.Migrations
{
    [DbContext(typeof(MvpContext))]
    [Migration("20190531000005_AddCascadeDeleteOnRooms")]
    partial class AddCascadeDeleteOnRooms
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("mvp")
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationId");

                    b.Property<int?>("OfficeId");

                    b.Property<string>("Title")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("MVP.Entities.Entities.ApartmentRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentId");

                    b.Property<int>("BedCount");

                    b.Property<int>("RoomNumber");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("ApartmentRoom");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentRoomId");

                    b.Property<DateTimeOffset>("End");

                    b.Property<DateTimeOffset>("Start");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentRoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("MVP.Entities.Entities.FlightInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost");

                    b.Property<DateTimeOffset>("End");

                    b.Property<string>("FromAirport")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Start");

                    b.Property<int>("Status");

                    b.Property<string>("ToAirport")
                        .IsRequired();

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("FlightInformation");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationId");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Office");
                });

            modelBuilder.Entity("MVP.Entities.Entities.RentalCarInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost");

                    b.Property<string>("DropOffAddress")
                        .IsRequired();

                    b.Property<DateTimeOffset>("End");

                    b.Property<string>("PickupAddress")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Start");

                    b.Property<int>("Status");

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("RentalCarInformation");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("End");

                    b.Property<int>("FromOfficeId");

                    b.Property<string>("OrganizerId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Start");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("ToOfficeId");

                    b.Property<int>("TripStatus");

                    b.HasKey("Id");

                    b.HasIndex("FromOfficeId");

                    b.HasIndex("OrganizerId");

                    b.HasIndex("ToOfficeId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("MVP.Entities.Entities.TripApartmentInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentRoomId");

                    b.Property<int>("CalendarId");

                    b.Property<int>("TripId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CalendarId")
                        .IsUnique();

                    b.ToTable("TripApartmentInfo");
                });

            modelBuilder.Entity("MVP.Entities.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MVP.Entities.Entities.UserTrip", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("TripId");

                    b.Property<bool>("Confirmed");

                    b.HasKey("UserId", "TripId");

                    b.HasIndex("TripId");

                    b.ToTable("UserTrip");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MVP.Entities.Entities.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MVP.Entities.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVP.Entities.Entities.User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MVP.Entities.Entities.User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVP.Entities.Entities.Apartment", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MVP.Entities.Entities.Office", "Office")
                        .WithMany("Apartments")
                        .HasForeignKey("OfficeId");
                });

            modelBuilder.Entity("MVP.Entities.Entities.ApartmentRoom", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Apartment")
                        .WithMany("Rooms")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVP.Entities.Entities.Calendar", b =>
                {
                    b.HasOne("MVP.Entities.Entities.ApartmentRoom", "ApartmentRoom")
                        .WithMany("Calendars")
                        .HasForeignKey("ApartmentRoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVP.Entities.Entities.User", "User")
                        .WithMany("Calendars")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MVP.Entities.Entities.FlightInformation", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Trip", "Trip")
                        .WithMany("FlightInformations")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVP.Entities.Entities.Office", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MVP.Entities.Entities.RentalCarInformation", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Trip", "Trip")
                        .WithMany("RentalCarInformations")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVP.Entities.Entities.Trip", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Office", "FromOffice")
                        .WithMany()
                        .HasForeignKey("FromOfficeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MVP.Entities.Entities.User", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MVP.Entities.Entities.Office", "ToOffice")
                        .WithMany()
                        .HasForeignKey("ToOfficeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MVP.Entities.Entities.TripApartmentInfo", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Calendar", "Calendar")
                        .WithOne("TripApartmentInfo")
                        .HasForeignKey("MVP.Entities.Entities.TripApartmentInfo", "CalendarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVP.Entities.Entities.UserTrip", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Trip", "Trip")
                        .WithMany("UserTrips")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVP.Entities.Entities.User", "User")
                        .WithMany("UserTrips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
