using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Domain.Migrations
{
    /// <inheritdoc />
    public partial class contactuspage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContactUs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ContactUs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ContactUs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ContactUs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "ContactUs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContactUs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ContactUs");
        }
    }
}
