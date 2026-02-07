using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_api.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeeleteuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoListItems_Users_UserId",
                table: "TodoListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoListItems_Users_UserId",
                table: "TodoListItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoListItems_Users_UserId",
                table: "TodoListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoListItems_Users_UserId",
                table: "TodoListItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
