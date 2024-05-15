using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Dbv21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    owner_id = table.Column<long>(type: "bigint", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    confidentiality = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item", x => x.item_id);
                    table.ForeignKey(
                        name: "FK_item_item_parent_id",
                        column: x => x.parent_id,
                        principalTable: "item",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "FK_item_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_comment",
                columns: table => new
                {
                    item_comment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    comment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_comment", x => x.item_comment_id);
                });

            migrationBuilder.CreateTable(
                name: "note",
                columns: table => new
                {
                    note_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    owner_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    like_count = table.Column<int>(type: "integer", nullable: false),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    project_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note", x => x.note_id);
                    table.ForeignKey(
                        name: "FK_note_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_note_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_note_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    read_level = table.Column<int>(type: "integer", nullable: false),
                    write_level = table.Column<int>(type: "integer", nullable: false),
                    create_level = table.Column<int>(type: "integer", nullable: false),
                    delete_level = table.Column<int>(type: "integer", nullable: false),
                    wiki_level = table.Column<int>(type: "integer", nullable: false),
                    wiki_delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_role_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wiki_page",
                columns: table => new
                {
                    wiki_page_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki_page", x => x.wiki_page_id);
                    table.ForeignKey(
                        name: "FK_wiki_page_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "note_rating",
                columns: table => new
                {
                    note_rating_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    note_id = table.Column<long>(type: "bigint", nullable: false),
                    rate = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note_rating", x => x.note_rating_id);
                    table.ForeignKey(
                        name: "FK_note_rating_note_note_id",
                        column: x => x.note_id,
                        principalTable: "note",
                        principalColumn: "note_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_note_rating_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_item_owner_id",
                table: "item",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_parent_id",
                table: "item",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_project_id",
                table: "item",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_department_id",
                table: "note",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_owner_id",
                table: "note",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_project_id",
                table: "note",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_rating_note_id",
                table: "note_rating",
                column: "note_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_rating_user_id",
                table: "note_rating",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_department_id",
                table: "role",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_wiki_page_project_id",
                table: "wiki_page",
                column: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "item_comment");

            migrationBuilder.DropTable(
                name: "note_rating");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "wiki_page");

            migrationBuilder.DropTable(
                name: "note");
        }
    }
}
