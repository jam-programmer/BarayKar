using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutLawinSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("dd58aa70-6f2a-4d3c-b44c-6e3e5bdcd053"));

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Law",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "About", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Law", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("b78bcf18-07d0-48bb-ab83-8f1b544a37bf"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("b78bcf18-07d0-48bb-ab83-8f1b544a37bf"));

            migrationBuilder.DropColumn(
                name: "About",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "Law",
                table: "Setting");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("dd58aa70-6f2a-4d3c-b44c-6e3e5bdcd053"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
