using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemHealthChecks.Infrastructure.Migrations
{
    public partial class addurlapicheckstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlApiHealthCheck",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostUrl = table.Column<string>(nullable: true),
                    TestApiPath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlApiHealthCheck", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlApiHealthCheck");
        }
    }
}
