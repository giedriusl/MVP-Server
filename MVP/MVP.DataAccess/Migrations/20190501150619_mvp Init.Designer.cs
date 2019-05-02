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
    [Migration("20190501150619_mvp Init")]
    partial class mvpInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("mvp")
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVP.Entities.Entities.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BedCount");

                    b.Property<int>("LocationId");

                    b.Property<int>("OfficeId");

                    b.Property<int?>("OfficeId1");

                    b.Property<string>("Title")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OfficeId");

                    b.HasIndex("OfficeId1");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("MVP.Entities.Entities.ApartmentRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<int>("AppartmentId");

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

                    b.Property<int>("ApartmentId");

                    b.Property<DateTimeOffset>("End");

                    b.Property<DateTimeOffset>("Start");

                    b.Property<int>("UserId");

                    b.Property<string>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("UserId1");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("MVP.Entities.Entities.FlightInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost");

                    b.Property<DateTimeOffset>("End");

                    b.Property<DateTimeOffset>("Start");

                    b.Property<int>("Status");

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
                        .HasMaxLength(500);

                    b.Property<string>("City")
                        .HasMaxLength(500);

                    b.Property<string>("CountryCode")
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

                    b.Property<int?>("LocationId1");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("LocationId1");

                    b.ToTable("Office");
                });

            modelBuilder.Entity("MVP.Entities.Entities.RentalCarInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost");

                    b.Property<DateTimeOffset>("End");

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

                    b.Property<DateTimeOffset>("Start");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("ToOfficeId");

                    b.Property<int>("TripStatus");

                    b.HasKey("Id");

                    b.HasIndex("FromOfficeId");

                    b.HasIndex("ToOfficeId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("MVP.Entities.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int?>("TripId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Apartment", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVP.Entities.Entities.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MVP.Entities.Entities.Office")
                        .WithMany("Apartments")
                        .HasForeignKey("OfficeId1")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MVP.Entities.Entities.ApartmentRoom", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Apartment")
                        .WithMany("Rooms")
                        .HasForeignKey("ApartmentId");
                });

            modelBuilder.Entity("MVP.Entities.Entities.Calendar", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVP.Entities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
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
                    b.HasOne("MVP.Entities.Entities.Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVP.Entities.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId1");
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

                    b.HasOne("MVP.Entities.Entities.Office", "ToOffice")
                        .WithMany()
                        .HasForeignKey("ToOfficeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MVP.Entities.Entities.User", b =>
                {
                    b.HasOne("MVP.Entities.Entities.Trip")
                        .WithMany("Users")
                        .HasForeignKey("TripId");
                });
#pragma warning restore 612, 618
        }
    }
}
