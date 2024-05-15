using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv254 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_level",
                table: "role");

            migrationBuilder.DropColumn(
                name: "delete_level",
                table: "role");

            migrationBuilder.DropColumn(
                name: "read_level",
                table: "role");

            migrationBuilder.RenameColumn(
                name: "write_level",
                table: "role",
                newName: "item_level");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "item_level",
                table: "role",
                newName: "write_level");

            migrationBuilder.AddColumn<int>(
                name: "create_level",
                table: "role",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "delete_level",
                table: "role",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "read_level",
                table: "role",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
