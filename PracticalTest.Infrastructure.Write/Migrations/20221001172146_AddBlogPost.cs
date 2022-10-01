using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticalTest.Infrastructure.Migrations
{
    public partial class AddBlogPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPost",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    BlogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPost_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "dbo",
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_BlogId",
                schema: "dbo",
                table: "BlogPost",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPost",
                schema: "dbo");
        }
    }
}
