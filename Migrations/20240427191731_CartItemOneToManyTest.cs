using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Migrations
{
    /// <inheritdoc />
    public partial class CartItemOneToManyTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Cart_CartId1",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_CartId1",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "Item");

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    ItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => new { x.CartsId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Item_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ItemsId",
                table: "CartItem",
                column: "ItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.AddColumn<int>(
                name: "CartId1",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_CartId1",
                table: "Item",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Cart_CartId1",
                table: "Item",
                column: "CartId1",
                principalTable: "Cart",
                principalColumn: "Id");
        }
    }
}
