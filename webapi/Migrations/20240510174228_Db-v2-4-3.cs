using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wiki_page_project_project_id",
                table: "wiki_page");

            migrationBuilder.DropColumn(
                name: "file_path",
                table: "wiki_page");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "wiki_page",
                newName: "department_id");

            migrationBuilder.RenameIndex(
                name: "IX_wiki_page_project_id",
                table: "wiki_page",
                newName: "IX_wiki_page_department_id");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "wiki_page",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "response",
                table: "department_invite",
                type: "integer",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<long>(
                name: "owner_id",
                table: "department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_department_owner_id",
                table: "department",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_department_user_owner_id",
                table: "department",
                column: "owner_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wiki_page_department_department_id",
                table: "wiki_page",
                column: "department_id",
                principalTable: "department",
                principalColumn: "department_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_user_owner_id",
                table: "department");

            migrationBuilder.DropForeignKey(
                name: "FK_wiki_page_department_department_id",
                table: "wiki_page");

            migrationBuilder.DropIndex(
                name: "IX_department_owner_id",
                table: "department");

            migrationBuilder.DropColumn(
                name: "content",
                table: "wiki_page");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "department");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "wiki_page",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "IX_wiki_page_department_id",
                table: "wiki_page",
                newName: "IX_wiki_page_project_id");

            migrationBuilder.AddColumn<string>(
                name: "file_path",
                table: "wiki_page",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "response",
                table: "department_invite",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_wiki_page_project_project_id",
                table: "wiki_page",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
