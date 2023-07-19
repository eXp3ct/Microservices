using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserConfigurationChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b5c58315-da76-4bc2-a9e6-736f36372896"));

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("d2226726-128e-46c7-9ed9-7ca826d81d9b"));

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("90e32ea4-ada6-489f-a373-48bafe8630af"), "Organization A" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MiddleName", "Name", "OrganizationId", "PhoneNumber", "Surname" },
                values: new object[] { new Guid("e9616521-2771-4a86-b48f-0aa41ae6b2d0"), "mail@mail.com", "", "Alex", new Guid("90e32ea4-ada6-489f-a373-48bafe8630af"), "1234567890", "Alex1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("d2226726-128e-46c7-9ed9-7ca826d81d9b"), "Organization A" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MiddleName", "Name", "OrganizationId", "PhoneNumber", "Surname" },
                values: new object[] { new Guid("b5c58315-da76-4bc2-a9e6-736f36372896"), "mail@mail.com", "", "Alex", new Guid("d2226726-128e-46c7-9ed9-7ca826d81d9b"), "1234567890", "Alex1" });
        }
    }
}
