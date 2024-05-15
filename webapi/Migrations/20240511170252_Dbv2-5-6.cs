using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv256 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confidentiality",
                table: "item_history");

            migrationBuilder.DropColumn(
                name: "deadline",
                table: "item_history");

            migrationBuilder.DropColumn(
                name: "confidentiality",
                table: "item");

            migrationBuilder.DropColumn(
                name: "deadline",
                table: "item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "confidentiality",
                table: "item_history",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deadline",
                table: "item_history",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "confidentiality",
                table: "item",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deadline",
                table: "item",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
