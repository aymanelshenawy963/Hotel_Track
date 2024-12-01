using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Track.Migrations
{
    /// <inheritdoc />
    public partial class addAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Id], [Discriminator], [FirstName], [LastName], [Address], [IsAdmin], [ProfilePicture], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8b56791a-2649-4a76-b59e-a1abece2e2f9', N'ApplicationUser', N'ayman', N'mohamed', N'tanta', 0, Null, N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGUTNXw77rZrxJbYOnu7Q0+QL8BqLvgiSY6yS1sIbGIeuBEoyq/cRWmOaUietP28kQ==', N'V2D5CI25WIJEK4WCU4VA474F25YPZMS3', N'6a55e84a-c380-482b-81ca-af7032e14343', NULL, 0, 0, NULL, 1, 0)\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FORM  [dbo].[Users] WHERE Id = '8b56791a-2649-4a76-b59e-a1abece2e2f9' ");

        }
    }
}
