using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticalTest.Transformations.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedOn",
                schema: "read",
                table: "BlogPosts",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                schema: "read",
                table: "BlogPosts",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "read",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostId = table.Column<long>(type: "bigint", nullable: false),
                    BlogPostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostReadModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostReadModelId",
                        column: x => x.BlogPostReadModelId,
                        principalSchema: "read",
                        principalTable: "BlogPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostReadModelId",
                schema: "read",
                table: "Comments",
                column: "BlogPostReadModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "read");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                schema: "read",
                table: "BlogPosts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "read",
                table: "BlogPosts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
