using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addaboutus1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AboutUs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AboutUs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AboutUs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AboutUs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AboutUs");
        }
    }
}
