using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectLunar.API.Migrations
{
    public partial class AddFilePropeties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Photos");
        }
    }
}
