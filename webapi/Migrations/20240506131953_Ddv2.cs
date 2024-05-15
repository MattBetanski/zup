using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Ddv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_department_department_id",
                table: "project");

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                table: "user",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<bool>(
                name: "account_activated",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "join_date",
                table: "user",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "department_id",
                table: "project",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "project_id",
                table: "project",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "project",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "project",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "department_id",
                table: "department",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_date",
                table: "department",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "department",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "visibility",
                table: "department",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "item_history",
                columns: table => new
                {
                    history_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    owner_id = table.Column<long>(type: "bigint", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    confidentiality = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false),
                    change = table.Column<string>(type: "text", nullable: false),
                    change_user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_item_history_user_change_user_id",
                        column: x => x.change_user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_history_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_item_history_change_user_id",
                table: "item_history",
                column: "change_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_history_owner_id",
                table: "item_history",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_department_department_id",
                table: "project",
                column: "department_id",
                principalTable: "department",
                principalColumn: "department_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_department_department_id",
                table: "project");

            migrationBuilder.DropTable(
                name: "item_history");

            migrationBuilder.DropColumn(
                name: "account_activated",
                table: "user");

            migrationBuilder.DropColumn(
                name: "join_date",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "user");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "project");

            migrationBuilder.DropColumn(
                name: "description",
                table: "project");

            migrationBuilder.DropColumn(
                name: "creation_date",
                table: "department");

            migrationBuilder.DropColumn(
                name: "description",
                table: "department");

            migrationBuilder.DropColumn(
                name: "visibility",
                table: "department");

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "user",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "project",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "project_id",
                table: "project",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "department",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_project_department_department_id",
                table: "project",
                column: "department_id",
                principalTable: "department",
                principalColumn: "department_id");
        }
    }
}
