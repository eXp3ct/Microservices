using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserNavigationPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9616521-2771-4a86-b48f-0aa41ae6b2d0"));

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("90e32ea4-ada6-489f-a373-48bafe8630af"));

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a2596350-6d2a-4362-80d6-151fcd43d4ab"), "Organization A" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MiddleName", "Name", "OrganizationId", "PhoneNumber", "Surname" },
                values: new object[] { new Guid("18bc6cb0-0111-4f03-aaad-2d53611715d0"), "mail@mail.com", "", "Alex", new Guid("a2596350-6d2a-4362-80d6-151fcd43d4ab"), "1234567890", "Alex1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("90e32ea4-ada6-489f-a373-48bafe8630af"), "Organization A" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MiddleName", "Name", "OrganizationId", "PhoneNumber", "Surname" },
                values: new object[] { new Guid("e9616521-2771-4a86-b48f-0aa41ae6b2d0"), "mail@mail.com", "", "Alex", new Guid("90e32ea4-ada6-489f-a373-48bafe8630af"), "1234567890", "Alex1" });
        }
    }
}
