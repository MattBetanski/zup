using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv257 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserRole_project_project_id",
                table: "ProjectUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserRole_role_role_id",
                table: "ProjectUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserRole_user_user_id",
                table: "ProjectUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUserRole",
                table: "ProjectUserRole");

            migrationBuilder.RenameTable(
                name: "ProjectUserRole",
                newName: "project_user_role");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUserRole_user_id",
                table: "project_user_role",
                newName: "IX_project_user_role_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUserRole_role_id",
                table: "project_user_role",
                newName: "IX_project_user_role_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_user_role",
                table: "project_user_role",
                columns: new[] { "project_id", "user_id", "role_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_project_user_role_project_project_id",
                table: "project_user_role",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_user_role_role_role_id",
                table: "project_user_role",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_user_role_user_user_id",
                table: "project_user_role",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_user_role_project_project_id",
                table: "project_user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_project_user_role_role_role_id",
                table: "project_user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_project_user_role_user_user_id",
                table: "project_user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_user_role",
                table: "project_user_role");

            migrationBuilder.RenameTable(
                name: "project_user_role",
                newName: "ProjectUserRole");

            migrationBuilder.RenameIndex(
                name: "IX_project_user_role_user_id",
                table: "ProjectUserRole",
                newName: "IX_ProjectUserRole_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_user_role_role_id",
                table: "ProjectUserRole",
                newName: "IX_ProjectUserRole_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUserRole",
                table: "ProjectUserRole",
                columns: new[] { "project_id", "user_id", "role_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserRole_project_project_id",
                table: "ProjectUserRole",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserRole_role_role_id",
                table: "ProjectUserRole",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserRole_user_user_id",
                table: "ProjectUserRole",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
