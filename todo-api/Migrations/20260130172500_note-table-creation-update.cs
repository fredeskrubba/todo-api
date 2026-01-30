using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_api.Migrations
{
    /// <inheritdoc />
    public partial class notetablecreationupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Notes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Notes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");
        }
    }
}
