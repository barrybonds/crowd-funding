using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowdFunding.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endeavours",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 60, nullable: false),
                    GoalAmount = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true),
                    TypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endeavours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endeavours_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endeavours_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Medical Emergency" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Community Project" }
                });

            migrationBuilder.InsertData(
                table: "Endeavours",
                columns: new[] { "Id", "CategoryId", "Description", "EndDate", "GoalAmount", "Name", "StartDate", "TypeId" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), null, "Seed Data Edeavour Description", new DateTime(2020, 9, 2, 10, 41, 16, 728, DateTimeKind.Local).AddTicks(5448), 0m, "Seed Data Endeavour Name", new DateTime(2020, 8, 3, 10, 41, 16, 728, DateTimeKind.Local).AddTicks(4550), null });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { new Guid("191cb04f-4bb0-4793-4570-08d7e9fd1fda"), "Self" });

            migrationBuilder.CreateIndex(
                name: "IX_Endeavours_CategoryId",
                table: "Endeavours",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Endeavours_TypeId",
                table: "Endeavours",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endeavours");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
