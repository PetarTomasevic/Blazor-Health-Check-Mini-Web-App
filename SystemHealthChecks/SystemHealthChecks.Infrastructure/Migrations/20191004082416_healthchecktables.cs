using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemHealthChecks.Infrastructure.Migrations
{
    public partial class healthchecktables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthCheckCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthCheckCategoryName = table.Column<string>(nullable: true),
                    HealthCheckCategoryDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheckCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthCheckSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthCheckSettingName = table.Column<string>(nullable: true),
                    HealthCheckSettingDescription = table.Column<string>(nullable: true),
                    HealthCheckSettingValue = table.Column<string>(nullable: true),
                    HealthCheckCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheckSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthCheckSettings_HealthCheckCategory_HealthCheckCategoryId",
                        column: x => x.HealthCheckCategoryId,
                        principalTable: "HealthCheckCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthCheckSettings_HealthCheckCategoryId",
                table: "HealthCheckSettings",
                column: "HealthCheckCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthCheckSettings");

            migrationBuilder.DropTable(
                name: "HealthCheckCategory");
        }
    }
}
