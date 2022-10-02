using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticalTest.Infrastructure.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                        SET IDENTITY_INSERT [dbo].[Users] ON 

                        IF NOT EXISTS (SELECT * FROM [dbo].[Users] WHERE email='test.user@gmail.com')
                        BEGIN
                            INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Role]) VALUES (1, N'test.user@gmail.com', N'test.user@gmail.com', N'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342', N'StandardUser')
                        END
                        IF NOT EXISTS (SELECT * FROM [dbo].[Users] WHERE email='test.user2@gmail.com')
                        BEGIN
                            INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Role]) VALUES (2, N'test.user2@gmail.com', N'test.user2@gmail.com', N'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342', N'StandardUser')
                        END
                        SET IDENTITY_INSERT [dbo].[Users] OFF
                        GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
