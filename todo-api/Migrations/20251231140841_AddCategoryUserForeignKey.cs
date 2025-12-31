using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_api.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryUserForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TodoListItems_CategoryId",
                table: "TodoListItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoListItems_Categories_CategoryId",
                table: "TodoListItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoListItems_Categories_CategoryId",
                table: "TodoListItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoListItems_CategoryId",
                table: "TodoListItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");
        }
    }
}
