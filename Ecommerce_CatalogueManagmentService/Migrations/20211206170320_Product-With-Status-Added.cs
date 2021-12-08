using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_CatalogueManagmentService.Migrations
{
    public partial class ProductWithStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblProductStatus",
                columns: table => new
                {
                    PsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductStatus", x => x.PsId);
                    table.ForeignKey(
                        name: "FK_TblProductStatus_TblProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblProductStatus_TblStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TblStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblProductStatus_ProductId",
                table: "TblProductStatus",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductStatus_StatusId",
                table: "TblProductStatus",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblProductStatus");
        }
    }
}
