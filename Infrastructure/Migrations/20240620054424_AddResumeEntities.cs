using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResumeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("4992d1a2-9b63-4614-a062-b239e1d51d0d"));

            migrationBuilder.EnsureSchema(
                name: "Cv");

            migrationBuilder.CreateTable(
                name: "Resume",
                schema: "Cv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MilitaryService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightsRequested = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resume_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationalRecord",
                schema: "Cv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStudying = table.Column<bool>(type: "bit", nullable: false),
                    ResumeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalRecord_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalSchema: "Cv",
                        principalTable: "Resume",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkExperience",
                schema: "Cv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false),
                    ResumeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkExperience_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalSchema: "Cv",
                        principalTable: "Resume",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("673bbf94-f56d-4ee0-bf97-703d717b0be5"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalRecord_ResumeId",
                schema: "Cv",
                table: "EducationalRecord",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_UserId",
                schema: "Cv",
                table: "Resume",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperience_ResumeId",
                schema: "Cv",
                table: "WorkExperience",
                column: "ResumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationalRecord",
                schema: "Cv");

            migrationBuilder.DropTable(
                name: "WorkExperience",
                schema: "Cv");

            migrationBuilder.DropTable(
                name: "Resume",
                schema: "Cv");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: new Guid("673bbf94-f56d-4ee0-bf97-703d717b0be5"));

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AboutSectionDescription", "AboutSectionTitle", "ApiNumber", "ApiSms", "Email", "Footer", "Header", "Instagram", "Linkdin", "Logo", "MainSubText", "MainText", "Number", "Telegram", "TitleSite", "WhatsApp" },
                values: new object[] { new Guid("4992d1a2-9b63-4614-a062-b239e1d51d0d"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        }
    }
}
