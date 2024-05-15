using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "role_id",
                table: "role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateTable(
                name: "department_member",
                columns: table => new
                {
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    department_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_member", x => new { x.member_id, x.department_id });
                    table.ForeignKey(
                        name: "FK_department_member_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_department_member_user_member_id",
                        column: x => x.member_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_assignee",
                columns: table => new
                {
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    assingee_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_assignee", x => new { x.item_id, x.assingee_id });
                    table.ForeignKey(
                        name: "FK_item_assignee_item_item_id",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_assignee_user_assingee_id",
                        column: x => x.assingee_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUserRole",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserRole", x => new { x.project_id, x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_ProjectUserRole_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUserRole_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUserRole_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_member_department_id",
                table: "department_member",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_assignee_assingee_id",
                table: "item_assignee",
                column: "assingee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserRole_role_id",
                table: "ProjectUserRole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserRole_user_id",
                table: "ProjectUserRole",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department_member");

            migrationBuilder.DropTable(
                name: "item_assignee");

            migrationBuilder.DropTable(
                name: "ProjectUserRole");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "role",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }
    }
}
