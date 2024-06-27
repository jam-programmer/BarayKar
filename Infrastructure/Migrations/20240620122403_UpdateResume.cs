using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("673bbf94-f56d-4ee0-bf97-703d717b0be5"));

            migrationBuilder.DropColumn(
                name: "Mail",
                schema: "Cv",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Cv",
                table: "Resume");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                schema: "Cv",
                table: "Resume",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("b8ff1b17-16de-4f38-b121-b35820b3a176"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("b8ff1b17-16de-4f38-b121-b35820b3a176"));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                schema: "Cv",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                schema: "Cv",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Cv",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("673bbf94-f56d-4ee0-bf97-703d717b0be5"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
