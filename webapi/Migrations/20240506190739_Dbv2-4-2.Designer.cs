﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webapi.Data;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(ZupContext))]
    [Migration("20240506190739_Dbv2-4-2")]
    partial class Dbv242
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("webapi.Models.Department", b =>
                {
                    b.Property<long>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("DepartmentId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<bool>("visibility")
                        .HasColumnType("boolean")
                        .HasColumnName("visibility");

                    b.HasKey("DepartmentId");

                    b.ToTable("department");
                });

            modelBuilder.Entity("webapi.Models.DepartmentInvite", b =>
                {
                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.Property<long>("InviteeId")
                        .HasColumnType("bigint")
                        .HasColumnName("invitee_id");

                    b.Property<bool>("Response")
                        .HasColumnType("boolean")
                        .HasColumnName("response");

                    b.HasKey("DepartmentId", "InviteeId");

                    b.HasIndex("InviteeId");

                    b.ToTable("department_invite");
                });

            modelBuilder.Entity("webapi.Models.DepartmentMember", b =>
                {
                    b.Property<long>("MemberId")
                        .HasColumnType("bigint")
                        .HasColumnName("member_id");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.HasKey("MemberId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("department_member");
                });

            modelBuilder.Entity("webapi.Models.Item", b =>
                {
                    b.Property<long>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("ItemId"));

                    b.Property<bool>("Confidentiality")
                        .HasColumnType("boolean")
                        .HasColumnName("confidentiality");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deadline");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint")
                        .HasColumnName("owner_id");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_id");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("ItemId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("item");
                });

            modelBuilder.Entity("webapi.Models.ItemAssignee", b =>
                {
                    b.Property<long>("ItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    b.Property<long>("AssingeeId")
                        .HasColumnType("bigint")
                        .HasColumnName("assingee_id");

                    b.HasKey("ItemId", "AssingeeId");

                    b.HasIndex("AssingeeId");

                    b.ToTable("item_assignee");
                });

            modelBuilder.Entity("webapi.Models.ItemComment", b =>
                {
                    b.Property<long>("ItemCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("item_comment_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("ItemCommentId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("comment_date");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("ItemCommentId");

                    b.ToTable("item_comment");
                });

            modelBuilder.Entity("webapi.Models.ItemHistory", b =>
                {
                    b.Property<long>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("history_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("HistoryId"));

                    b.Property<string>("Change")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("change");

                    b.Property<long>("ChangeUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("change_user_id");

                    b.Property<bool>("Confidentiality")
                        .HasColumnType("boolean")
                        .HasColumnName("confidentiality");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deadline");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<long?>("ItemId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint")
                        .HasColumnName("owner_id");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_id");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("HistoryId");

                    b.HasIndex("ChangeUserId");

                    b.HasIndex("OwnerId");

                    b.ToTable("item_history");
                });

            modelBuilder.Entity("webapi.Models.Note", b =>
                {
                    b.Property<long>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("note_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("NoteId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.Property<int>("LikeCount")
                        .HasColumnType("integer")
                        .HasColumnName("like_count");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint")
                        .HasColumnName("owner_id");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("NoteId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("note");
                });

            modelBuilder.Entity("webapi.Models.NoteRating", b =>
                {
                    b.Property<long>("NoteRatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("note_rating_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("NoteRatingId"));

                    b.Property<long>("NoteId")
                        .HasColumnType("bigint")
                        .HasColumnName("note_id");

                    b.Property<bool>("Rate")
                        .HasColumnType("boolean")
                        .HasColumnName("rate");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("NoteRatingId");

                    b.HasIndex("NoteId");

                    b.HasIndex("UserId");

                    b.ToTable("note_rating");
                });

            modelBuilder.Entity("webapi.Models.Project", b =>
                {
                    b.Property<long>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("ProjectId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ProjectId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("project");
                });

            modelBuilder.Entity("webapi.Models.ProjectUserRole", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("ProjectId", "UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUserRole");
                });

            modelBuilder.Entity("webapi.Models.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("RoleId"));

                    b.Property<int>("CreateLevel")
                        .HasColumnType("integer")
                        .HasColumnName("create_level");

                    b.Property<int>("DeleteLevel")
                        .HasColumnType("integer")
                        .HasColumnName("delete_level");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("ReadLevel")
                        .HasColumnType("integer")
                        .HasColumnName("read_level");

                    b.Property<bool>("WikiDelete")
                        .HasColumnType("boolean")
                        .HasColumnName("wiki_delete");

                    b.Property<int>("WikiLevel")
                        .HasColumnType("integer")
                        .HasColumnName("wiki_level");

                    b.Property<int>("WriteLevel")
                        .HasColumnType("integer")
                        .HasColumnName("write_level");

                    b.HasKey("RoleId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("role");
                });

            modelBuilder.Entity("webapi.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("UserId"));

                    b.Property<bool>("AccountActivated")
                        .HasColumnType("boolean")
                        .HasColumnName("account_activated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("join_date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("webapi.Models.WikiPage", b =>
                {
                    b.Property<long>("WikiPageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("wiki_page_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("WikiPageId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("WikiPageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("wiki_page");
                });

            modelBuilder.Entity("webapi.Models.DepartmentInvite", b =>
                {
                    b.HasOne("webapi.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("InviteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.Models.DepartmentMember", b =>
                {
                    b.HasOne("webapi.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.Models.Item", b =>
                {
                    b.HasOne("webapi.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Item", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("webapi.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Parent");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("webapi.Models.ItemAssignee", b =>
                {
                    b.HasOne("webapi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("AssingeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.Models.ItemHistory", b =>
                {
                    b.HasOne("webapi.Models.User", "ChangeUser")
                        .WithMany("ChangedItemHistories")
                        .HasForeignKey("ChangeUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.User", "Owner")
                        .WithMany("OwnedItemHistories")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChangeUser");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("webapi.Models.Note", b =>
                {
                    b.HasOne("webapi.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Owner");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("webapi.Models.NoteRating", b =>
                {
                    b.HasOne("webapi.Models.Note", "Note")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.Models.Project", b =>
                {
                    b.HasOne("webapi.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("webapi.Models.ProjectUserRole", b =>
                {
                    b.HasOne("webapi.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.Models.Role", b =>
                {
                    b.HasOne("webapi.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("webapi.Models.WikiPage", b =>
                {
                    b.HasOne("webapi.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("webapi.Models.User", b =>
                {
                    b.Navigation("ChangedItemHistories");

                    b.Navigation("OwnedItemHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
