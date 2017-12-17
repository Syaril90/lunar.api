using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectLunar.API.Models;

namespace ProjectLunar.API.Migrations
{
    [DbContext(typeof(LunarContext))]
    [Migration("20170717131259_FixedLatitudLongitud")]
    partial class FixedLatitudLongitud
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectLunar.API.Models.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("File");

                    b.Property<DateTime?>("InsAt");

                    b.Property<string>("InsBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("PrayerPlaceId");

                    b.Property<DateTime?>("UpdAt");

                    b.Property<string>("UpdBy");

                    b.HasKey("Id");

                    b.HasIndex("PrayerPlaceId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ProjectLunar.API.Models.PrayerPlace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Category");

                    b.Property<DateTime?>("InsAt");

                    b.Property<string>("InsBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("Latitud");

                    b.Property<double>("Longitud");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdAt");

                    b.Property<string>("UpdBy");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PrayingPlaces");
                });

            modelBuilder.Entity("ProjectLunar.API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthFrom");

                    b.Property<string>("AuthId");

                    b.Property<DateTime?>("InsAt");

                    b.Property<string>("InsBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("UpdAt");

                    b.Property<string>("UpdBy");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectLunar.API.Models.UserAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("InsAt");

                    b.Property<string>("InsBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("PrayerPlaceId");

                    b.Property<DateTime?>("UpdAt");

                    b.Property<string>("UpdBy");

                    b.Property<Guid>("UserId");

                    b.Property<bool?>("isDislike");

                    b.Property<bool?>("isLike");

                    b.HasKey("Id");

                    b.HasIndex("PrayerPlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActions");
                });

            modelBuilder.Entity("ProjectLunar.API.Models.Photo", b =>
                {
                    b.HasOne("ProjectLunar.API.Models.PrayerPlace", "PrayerPlace")
                        .WithMany("Photos")
                        .HasForeignKey("PrayerPlaceId");
                });

            modelBuilder.Entity("ProjectLunar.API.Models.PrayerPlace", b =>
                {
                    b.HasOne("ProjectLunar.API.Models.User", "User")
                        .WithMany("PrayerPlaces")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProjectLunar.API.Models.UserAction", b =>
                {
                    b.HasOne("ProjectLunar.API.Models.PrayerPlace", "PrayerPlace")
                        .WithMany("UserActions")
                        .HasForeignKey("PrayerPlaceId");

                    b.HasOne("ProjectLunar.API.Models.User", "User")
                        .WithMany("UserActions")
                        .HasForeignKey("UserId");
                });
        }
    }
}
