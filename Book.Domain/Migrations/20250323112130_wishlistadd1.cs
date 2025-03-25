using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Domain.Migrations
{
    /// <inheritdoc />
    public partial class wishlistadd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookitemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationuserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishList_BookItems_BookitemId",
                        column: x => x.BookitemId,
                        principalTable: "BookItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishList_BookitemId",
                table: "WishList",
                column: "BookitemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishList");
        }
    }
}
