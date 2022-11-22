using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practise.Migrations
{
    public partial class AddWishlistAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishListProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WishListId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListProduct_WishList_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishListProduct_ProductId",
                table: "WishListProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListProduct_WishListId",
                table: "WishListProduct",
                column: "WishListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishListProduct");

            migrationBuilder.DropTable(
                name: "WishList");
        }
    }
}
