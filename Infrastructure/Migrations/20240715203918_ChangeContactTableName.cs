using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContactTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactEntity",
                table: "ContactEntity");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("63cbae5f-22c9-43ce-88d1-4b757cdbb7f8"));

            migrationBuilder.RenameTable(
                name: "ContactEntity",
                newName: "Contact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("dd58aa70-6f2a-4d3c-b44c-6e3e5bdcd053"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("dd58aa70-6f2a-4d3c-b44c-6e3e5bdcd053"));

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "ContactEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactEntity",
                table: "ContactEntity",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("63cbae5f-22c9-43ce-88d1-4b757cdbb7f8"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
