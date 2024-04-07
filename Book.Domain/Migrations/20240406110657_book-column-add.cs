using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Domain.Migrations
{
    /// <inheritdoc />
    public partial class bookcolumnadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "BookItems",
                newName: "FrontImage");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "BookItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "BookItems");

            migrationBuilder.RenameColumn(
                name: "FrontImage",
                table: "BookItems",
                newName: "ImageUrl");
        }
    }
}
