using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyToResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("b8ff1b17-16de-4f38-b121-b35820b3a176"));

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("d2e1eaca-acaf-43e4-b802-562af115ddc0"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("d2e1eaca-acaf-43e4-b802-562af115ddc0"));

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("b8ff1b17-16de-4f38-b121-b35820b3a176"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
