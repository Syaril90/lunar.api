using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectLunar.API.Migrations
{
    public partial class AddBaseModelInUSer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsAt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsBy",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdAt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdBy",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InsBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdBy",
                table: "Users");
        }
    }
}
