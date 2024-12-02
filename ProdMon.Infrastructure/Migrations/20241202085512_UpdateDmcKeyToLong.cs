using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdMon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDmcKeyToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCodes",
                columns: table => new
                {
                    ArticleNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCodes", x => x.ArticleNumber);
                });

            migrationBuilder.CreateTable(
                name: "MonitorEntries",
                columns: table => new
                {
                    Dmc = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckPointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorEntries", x => x.Dmc);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCodes");

            migrationBuilder.DropTable(
                name: "MonitorEntries");
        }
    }
}
