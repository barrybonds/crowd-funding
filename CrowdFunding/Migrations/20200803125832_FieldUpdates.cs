using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowdFunding.Migrations
{
    public partial class FieldUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Endeavours",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Endeavours",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Endeavours",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 9, 2, 13, 58, 31, 938, DateTimeKind.Local).AddTicks(2113), new DateTime(2020, 8, 3, 13, 58, 31, 938, DateTimeKind.Local).AddTicks(1345) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Endeavours",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Endeavours",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Endeavours",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 9, 2, 10, 41, 16, 728, DateTimeKind.Local).AddTicks(5448), new DateTime(2020, 8, 3, 10, 41, 16, 728, DateTimeKind.Local).AddTicks(4550) });
        }
    }
}
