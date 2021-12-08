using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_CatalogueManagmentService.Migrations
{
    public partial class ProductWithStatusAddedV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TblProductStatus_ProductId",
                table: "TblProductStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductStatus_ProductId",
                table: "TblProductStatus",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TblProductStatus_ProductId",
                table: "TblProductStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductStatus_ProductId",
                table: "TblProductStatus",
                column: "ProductId");
        }
    }
}
