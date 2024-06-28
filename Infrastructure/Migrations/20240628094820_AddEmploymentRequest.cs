using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmploymentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("e239559f-55e4-45cc-92b8-a58599b0df6f"));

            migrationBuilder.CreateTable(
                name: "EmploymentRequest",
                schema: "Emp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmploymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResumeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentRequest_Employment_EmploymentId",
                        column: x => x.EmploymentId,
                        principalSchema: "Emp",
                        principalTable: "Employment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmploymentRequest_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalSchema: "Cv",
                        principalTable: "Resume",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("9452cc01-ef5d-4db7-97c8-b0312dff0152"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentRequest_EmploymentId",
                schema: "Emp",
                table: "EmploymentRequest",
                column: "EmploymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentRequest_ResumeId",
                schema: "Emp",
                table: "EmploymentRequest",
                column: "ResumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentRequest",
                schema: "Emp");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("9452cc01-ef5d-4db7-97c8-b0312dff0152"));

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "Address", "ApiNumber", "ApiSms", "BusinessText", "CategoryText", "Email", "EmploymentText", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "ProvinceText", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("e239559f-55e4-45cc-92b8-a58599b0df6f"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
