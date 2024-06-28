using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyToSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("1ff50db7-7659-4696-85dc-2269ff867ec1"));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessText",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryText",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentText",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceText",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("e239559f-55e4-45cc-92b8-a58599b0df6f"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("e239559f-55e4-45cc-92b8-a58599b0df6f"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "BusinessText",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "CategoryText",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "EmploymentText",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "ProvinceText",
                table: "Setting");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("1ff50db7-7659-4696-85dc-2269ff867ec1"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
