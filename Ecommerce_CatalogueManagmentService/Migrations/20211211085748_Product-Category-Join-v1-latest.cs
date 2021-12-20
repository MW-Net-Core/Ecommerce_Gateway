using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_CatalogueManagmentService.Migrations
{
    public partial class ProductCategoryJoinv1latest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_TblCategory_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_TblProduct_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                newName: "TblProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_ProductId",
                table: "TblProductCategory",
                newName: "IX_TblProductCategory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "TblProductCategory",
                newName: "IX_TblProductCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblProductCategory",
                table: "TblProductCategory",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblProductCategory_TblCategory_CategoryId",
                table: "TblProductCategory",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblProductCategory_TblProduct_ProductId",
                table: "TblProductCategory",
                column: "ProductId",
                principalTable: "TblProduct",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblProductCategory_TblCategory_CategoryId",
                table: "TblProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TblProductCategory_TblProduct_ProductId",
                table: "TblProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblProductCategory",
                table: "TblProductCategory");

            migrationBuilder.RenameTable(
                name: "TblProductCategory",
                newName: "ProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_TblProductCategory_ProductId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TblProductCategory_CategoryId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_TblCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_TblProduct_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                principalTable: "TblProduct",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
