using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticalTest.Infrastructure.Migrations
{
    public partial class SeedTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            SET IDENTITY_INSERT [dbo].[Tags] ON 
                    INSERT [dbo].[Tags] ([Id], [Name], [CreatedOn], [ModifiedOn]) VALUES (1, N'Angular', CAST(N'2022-10-02T12:06:19.2600000+00:00' AS DateTimeOffset), CAST(N'2022-10-02T12:06:19.2600000+00:00' AS DateTimeOffset))
                    INSERT [dbo].[Tags] ([Id], [Name], [CreatedOn], [ModifiedOn]) VALUES (2, N'C#', CAST(N'2022-10-02T12:06:21.5766667+00:00' AS DateTimeOffset), CAST(N'2022-10-02T12:06:21.5766667+00:00' AS DateTimeOffset))
                    INSERT [dbo].[Tags] ([Id], [Name], [CreatedOn], [ModifiedOn]) VALUES (3, N'Java', CAST(N'2022-10-02T12:06:24.0000000+00:00' AS DateTimeOffset), CAST(N'2022-10-02T12:06:24.0000000+00:00' AS DateTimeOffset))
                    INSERT [dbo].[Tags] ([Id], [Name], [CreatedOn], [ModifiedOn]) VALUES (4, N'react', CAST(N'2022-10-02T12:06:25.7333333+00:00' AS DateTimeOffset), CAST(N'2022-10-02T12:06:25.7333333+00:00' AS DateTimeOffset))
                    SET IDENTITY_INSERT [dbo].[Tags] OFF
                    GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
