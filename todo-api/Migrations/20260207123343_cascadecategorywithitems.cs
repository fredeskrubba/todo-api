using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_api.Migrations
{
    /// <inheritdoc />
    public partial class cascadecategorywithitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoListItems_Categories_CategoryId",
                table: "TodoListItems");

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
                name: "FK_TodoListItems_Categories_CategoryId",
                table: "TodoListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoListItems_Categories_CategoryId",
                table: "TodoListItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
