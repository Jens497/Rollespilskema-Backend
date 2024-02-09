using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoleplayingSchemaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0727da0f-7ee2-493c-b2df-5bd5abdad3e8", "1", "Admin", "Admin" },
                    { "bbf86f36-bc90-4a19-bf7d-fd1d14a94b5f", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0727da0f-7ee2-493c-b2df-5bd5abdad3e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbf86f36-bc90-4a19-bf7d-fd1d14a94b5f");
        }
    }
}
