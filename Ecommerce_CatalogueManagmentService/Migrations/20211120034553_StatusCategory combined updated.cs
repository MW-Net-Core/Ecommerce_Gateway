using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_CatalogueManagmentService.Migrations
{
    public partial class StatusCategorycombinedupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TblCategoryStatus_CategoryId",
                table: "TblCategoryStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryStatus_CategoryId",
                table: "TblCategoryStatus",
                column: "CategoryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TblCategoryStatus_CategoryId",
                table: "TblCategoryStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryStatus_CategoryId",
                table: "TblCategoryStatus",
                column: "CategoryId");
        }
    }
}
