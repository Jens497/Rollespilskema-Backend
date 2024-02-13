using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoleplayingSchemaBackend.Migrations
{
    /// <inheritdoc />
    public partial class SheetWithTemplateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_Template_TemplateId",
                table: "Sheet");

            migrationBuilder.DropIndex(
                name: "IX_Sheet_TemplateId",
                table: "Sheet");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "134a69bf-0812-461f-9bf6-cc1a61331df1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f50d47b7-a3a7-44a1-85ad-e54987bf4587");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateId",
                table: "Sheet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "233a02ee-aa97-4688-a9a2-df62ae6a2e7a", "2", "User", "User" },
                    { "35be8206-2846-4512-8ca5-b2def6835ee4", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "233a02ee-aa97-4688-a9a2-df62ae6a2e7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35be8206-2846-4512-8ca5-b2def6835ee4");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "Sheet",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "134a69bf-0812-461f-9bf6-cc1a61331df1", "2", "User", "User" },
                    { "f50d47b7-a3a7-44a1-85ad-e54987bf4587", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_TemplateId",
                table: "Sheet",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_Template_TemplateId",
                table: "Sheet",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "TemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
