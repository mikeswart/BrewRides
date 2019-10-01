﻿// <auto-generated />
using System;
using CapitalBreweryBikeClub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CapitalBreweryBikeClub.Migrations
{
    [DbContext(typeof(RouteDatabaseContext))]
    partial class RouteDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-rc1.19456.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CapitalBreweryBikeClub.Model.MemberInformation", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Email");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Email = "mike.swart@gmail.com"
                        });
                });

            modelBuilder.Entity("CapitalBreweryBikeClub.Model.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByEmail");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("CapitalBreweryBikeClub.Model.RouteData", b =>
                {
                    b.Property<string>("RideWithGpsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mileage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RideWithGpsId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("CapitalBreweryBikeClub.Model.SiteState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("SiteState");

                    b.HasData(
                        new
                        {
                            Id = 1
                        });
                });

            modelBuilder.Entity("CapitalBreweryBikeClub.Model.Note", b =>
                {
                    b.HasOne("CapitalBreweryBikeClub.Model.MemberInformation", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CapitalBreweryBikeClub.Model.SiteState", b =>
                {
                    b.HasOne("CapitalBreweryBikeClub.Model.Note", "Note")
                        .WithMany()
                        .HasForeignKey("NoteId");
                });
#pragma warning restore 612, 618
        }
    }
}