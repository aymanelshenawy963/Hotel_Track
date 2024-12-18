using Hotel_Track.Utility;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
      table: "Roles",
      columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
      values: new object[]
      { Guid.NewGuid().ToString(), $"{SD.Admin}", $"{SD.Admin}".ToUpper(), Guid.NewGuid().ToString() }
                                     );
            migrationBuilder.InsertData(
               table: "Roles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[]
               { Guid.NewGuid().ToString(), $"{SD.Company}", $"{SD.Company}".ToUpper(), Guid.NewGuid().ToString() }
                                       );
            migrationBuilder.InsertData(
               table: "Roles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[]
               { Guid.NewGuid().ToString(), $"{SD.Customer}", $"{SD.Customer}".ToUpper(), Guid.NewGuid().ToString() }
                                       );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Roles]");
        }
    }
}
