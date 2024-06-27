using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyInEmployment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("b7255b48-b2a7-4b88-8091-8b415d275ed6"));

            migrationBuilder.AddColumn<string>(
                name: "BusinessDays",
                schema: "Emp",
                table: "Employment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("4992d1a2-9b63-4614-a062-b239e1d51d0d"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("4992d1a2-9b63-4614-a062-b239e1d51d0d"));

            migrationBuilder.DropColumn(
                name: "BusinessDays",
                schema: "Emp",
                table: "Employment");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("b7255b48-b2a7-4b88-8091-8b415d275ed6"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
