using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectLunar.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthFrom = table.Column<string>(nullable: true),
                    AuthId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrayingPlaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    InsAt = table.Column<DateTime>(nullable: true),
                    InsBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Latitud = table.Column<int>(nullable: true),
                    Longitud = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UpdAt = table.Column<DateTime>(nullable: true),
                    UpdBy = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayingPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrayingPlaces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    File = table.Column<byte[]>(nullable: true),
                    InsAt = table.Column<DateTime>(nullable: true),
                    InsBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PrayerPlaceId = table.Column<Guid>(nullable: false),
                    UpdAt = table.Column<DateTime>(nullable: true),
                    UpdBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_PrayingPlaces_PrayerPlaceId",
                        column: x => x.PrayerPlaceId,
                        principalTable: "PrayingPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsAt = table.Column<DateTime>(nullable: true),
                    InsBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PrayerPlaceId = table.Column<Guid>(nullable: false),
                    UpdAt = table.Column<DateTime>(nullable: true),
                    UpdBy = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    isDislike = table.Column<bool>(nullable: true),
                    isLike = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActions_PrayingPlaces_PrayerPlaceId",
                        column: x => x.PrayerPlaceId,
                        principalTable: "PrayingPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PrayerPlaceId",
                table: "Photos",
                column: "PrayerPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PrayingPlaces_UserId",
                table: "PrayingPlaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_PrayerPlaceId",
                table: "UserActions",
                column: "PrayerPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_UserId",
                table: "UserActions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "PrayingPlaces");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
