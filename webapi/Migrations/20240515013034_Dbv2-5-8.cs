using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv258 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_note_project_project_id",
                table: "note");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "note",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_note_project_id",
                table: "note",
                newName: "IX_note_ProjectId");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectId",
                table: "note",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_note_project_ProjectId",
                table: "note",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_note_project_ProjectId",
                table: "note");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "note",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "IX_note_ProjectId",
                table: "note",
                newName: "IX_note_project_id");

            migrationBuilder.AlterColumn<long>(
                name: "project_id",
                table: "note",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_note_project_project_id",
                table: "note",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
