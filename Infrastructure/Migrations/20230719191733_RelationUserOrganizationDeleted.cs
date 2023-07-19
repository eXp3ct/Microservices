using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationUserOrganizationDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("18bc6cb0-0111-4f03-aaad-2d53611715d0"));

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("a2596350-6d2a-4362-80d6-151fcd43d4ab"));

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ed048bcf-97af-4b42-8d9c-e97b3d1e086c"), "Organization A" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MiddleName", "Name", "OrganizationId", "PhoneNumber", "Surname" },
                values: new object[] { new Guid("e0952381-b9b8-4fcd-be99-b00f4f799b65"), "mail@mail.com", "", "Alex", new Guid("ed048bcf-97af-4b42-8d9c-e97b3d1e086c"), "1234567890", "Alex1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("ed048bcf-97af-4b42-8d9c-e97b3d1e086c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e0952381-b9b8-4fcd-be99-b00f4f799b65"));

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a2596350-6d2a-4362-80d6-151fcd43d4ab"), "Organization A" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MiddleName", "Name", "OrganizationId", "PhoneNumber", "Surname" },
                values: new object[] { new Guid("18bc6cb0-0111-4f03-aaad-2d53611715d0"), "mail@mail.com", "", "Alex", new Guid("a2596350-6d2a-4362-80d6-151fcd43d4ab"), "1234567890", "Alex1" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }
    }
}
