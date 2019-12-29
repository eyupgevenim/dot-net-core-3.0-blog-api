using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    Body = table.Column<string>(nullable: false),
                    BodyOverview = table.Column<string>(maxLength: 500, nullable: false),
                    AllowComments = table.Column<bool>(nullable: false, defaultValue: true),
                    Tags = table.Column<string>(maxLength: 100, nullable: false),
                    StartDateUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EndDateUtc = table.Column<DateTime>(nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 50, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(maxLength: 50, nullable: false),
                    Salt = table.Column<string>(maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(maxLength: 500, nullable: false),
                    IsApproved = table.Column<bool>(nullable: false, defaultValue: true),
                    PostId = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostComment_PostId",
                table: "PostComment",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostComment");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
