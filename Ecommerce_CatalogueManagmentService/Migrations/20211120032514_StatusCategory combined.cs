using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_CatalogueManagmentService.Migrations
{
    public partial class StatusCategorycombined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCategoryStatus",
                columns: table => new
                {
                    CsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategoryStatus", x => x.CsId);
                    table.ForeignKey(
                        name: "FK_TblCategoryStatus_TblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblCategoryStatus_TblStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TblStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryStatus_CategoryId",
                table: "TblCategoryStatus",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryStatus_StatusId",
                table: "TblCategoryStatus",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblCategoryStatus");
        }
    }
}
