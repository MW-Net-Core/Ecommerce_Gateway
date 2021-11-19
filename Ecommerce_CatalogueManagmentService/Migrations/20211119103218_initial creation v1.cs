using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_CatalogueManagmentService.Migrations
{
    public partial class initialcreationv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCategory_TblStatus_StatusId",
                table: "TblCategory");

            migrationBuilder.DropTable(
                name: "TblPCS");

            migrationBuilder.DropIndex(
                name: "IX_TblCategory_StatusId",
                table: "TblCategory");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TblCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "TblCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TblPCS",
                columns: table => new
                {
                    PscId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPCS", x => x.PscId);
                    table.ForeignKey(
                        name: "FK_TblPCS_TblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblPCS_TblProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblCategory_StatusId",
                table: "TblCategory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPCS_CategoryId",
                table: "TblPCS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPCS_ProductId",
                table: "TblPCS",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCategory_TblStatus_StatusId",
                table: "TblCategory",
                column: "StatusId",
                principalTable: "TblStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
