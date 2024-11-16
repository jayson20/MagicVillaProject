﻿// <auto-generated />
using System;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241115213821_SeedVillaTableAddedDateCreated")]
    partial class SeedVillaTableAddedDateCreated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqrft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 11, 15, 22, 38, 21, 91, DateTimeKind.Local).AddTicks(8270),
                            Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                            ImageUrl = "https://media.istockphoto.com/id/1929345158/photo/modern-apartment-with-large-windows.webp?s=2048x2048&w=is&k=20&c=BVoaNU0lklmbqhVmwt7Ospi8ecdVkap-NmrvCNuRUyY=",
                            Name = "Royal Villa",
                            Occupancy = 1,
                            Rate = 200.0,
                            Sqrft = 200,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 11, 15, 22, 38, 21, 91, DateTimeKind.Local).AddTicks(8282),
                            Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                            ImageUrl = "https://media.istockphoto.com/id/1929345158/photo/modern-apartment-with-large-windows.webp?s=2048x2048&w=is&k=20&c=BVoaNU0lklmbqhVmwt7Ospi8ecdVkap-NmrvCNuRUyY=",
                            Name = "Pool Villa",
                            Occupancy = 3,
                            Rate = 800.0,
                            Sqrft = 700,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 11, 15, 22, 38, 21, 91, DateTimeKind.Local).AddTicks(8284),
                            Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                            ImageUrl = "https://media.istockphoto.com/id/1929345158/photo/modern-apartment-with-large-windows.webp?s=2048x2048&w=is&k=20&c=BVoaNU0lklmbqhVmwt7Ospi8ecdVkap-NmrvCNuRUyY=",
                            Name = "Kings Villa",
                            Occupancy = 5,
                            Rate = 1200.0,
                            Sqrft = 1000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
