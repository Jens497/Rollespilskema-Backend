using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoleplayingSchemaBackend.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_AspNetUsers_UserId",
                table: "Sheet");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f33ff2c-407f-4c20-98e9-a401062ee729");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9894076b-aae7-414b-95a8-91be0faa8c58");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Sheet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "046acfe7-4bd1-41aa-b8b6-da79496b41d2", "1", "Admin", "Admin" },
                    { "0886367b-df02-481b-b30e-d87e26494028", "2", "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_AspNetUsers_UserId",
                table: "Sheet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_AspNetUsers_UserId",
                table: "Sheet");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "046acfe7-4bd1-41aa-b8b6-da79496b41d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0886367b-df02-481b-b30e-d87e26494028");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Sheet",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f33ff2c-407f-4c20-98e9-a401062ee729", "1", "Admin", "Admin" },
                    { "9894076b-aae7-414b-95a8-91be0faa8c58", "2", "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_AspNetUsers_UserId",
                table: "Sheet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
