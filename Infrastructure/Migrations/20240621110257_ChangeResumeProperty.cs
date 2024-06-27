using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeResumeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("d2e1eaca-acaf-43e4-b802-562af115ddc0"));

            migrationBuilder.AddColumn<string>(
                name: "Linkdin",
                schema: "Cv",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkRegistration",
                schema: "Cv",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("1ff50db7-7659-4696-85dc-2269ff867ec1"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("1ff50db7-7659-4696-85dc-2269ff867ec1"));

            migrationBuilder.DropColumn(
                name: "Linkdin",
                schema: "Cv",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "WorkRegistration",
                schema: "Cv",
                table: "Resume");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("d2e1eaca-acaf-43e4-b802-562af115ddc0"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
