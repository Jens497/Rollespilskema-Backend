using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoleplayingSchemaBackend.Migrations
{
    /// <inheritdoc />
    public partial class GetSheetByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "233a02ee-aa97-4688-a9a2-df62ae6a2e7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35be8206-2846-4512-8ca5-b2def6835ee4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sheet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f33ff2c-407f-4c20-98e9-a401062ee729", "1", "Admin", "Admin" },
                    { "9894076b-aae7-414b-95a8-91be0faa8c58", "2", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_UserId",
                table: "Sheet",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_AspNetUsers_UserId",
                table: "Sheet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_AspNetUsers_UserId",
                table: "Sheet");

            migrationBuilder.DropIndex(
                name: "IX_Sheet_UserId",
                table: "Sheet");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f33ff2c-407f-4c20-98e9-a401062ee729");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9894076b-aae7-414b-95a8-91be0faa8c58");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sheet");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "233a02ee-aa97-4688-a9a2-df62ae6a2e7a", "2", "User", "User" },
                    { "35be8206-2846-4512-8ca5-b2def6835ee4", "1", "Admin", "Admin" }
                });
        }
    }
}
