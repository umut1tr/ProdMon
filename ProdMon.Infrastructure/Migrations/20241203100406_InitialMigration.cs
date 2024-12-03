using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProdMon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCodes",
                columns: table => new
                {
                    ArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Dmc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckPointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorEntries", x => x.Dmc);
                });

            migrationBuilder.InsertData(
                table: "ArticleCodes",
                columns: new[] { "ArticleNumber", "ArticleDescription" },
                values: new object[,]
                {
                    { "05010292", "Bremsscheibe" },
                    { "123455", "Test" },
                    { "55322234", "Querlenker" }
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
