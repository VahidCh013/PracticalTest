using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticalTest.Infrastructure.Read.Migrations
{
    public partial class ReadUserIdentSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                        USE [PracticalTestDb]
                                        GO
                                        SET IDENTITY_INSERT [read].[Users] ON 

                                        IF NOT EXISTS (SELECT * FROM [read].[Users] WHERE email='test.user@gmail.com')
                                        BEGIN
                                        INSERT [read].[Users] ([Id], [UserName], [Email], [Password], [Role]) VALUES (1, N'test.user@gmail.com', N'test.user@gmail.com', N'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342', N'StandardUser')
                                        SET IDENTITY_INSERT [read].[Users] OFF
                                        END
                                        GO

                                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
