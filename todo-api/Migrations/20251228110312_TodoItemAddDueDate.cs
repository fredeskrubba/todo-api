using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_api.Migrations
{
    /// <inheritdoc />
    public partial class TodoItemAddDueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AddColumn<DateTime>(
           name: "DueDate",
           table: "TodoListItems",
           type: "datetime(6)",
           nullable: false,
           defaultValue: DateTime.MinValue
           );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "DueDate",
            table: "TodoListItems"
        );
        }
    }
}
