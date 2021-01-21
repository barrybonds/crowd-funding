using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowdFunding.Migrations
{
    public partial class AddRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8eb5cc0d-1214-47b1-80b9-42dfa3fc3008", "e565b565-cd72-4244-aeca-ee3879f44441", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a651393-b1ca-4818-a0cd-f2904b536a1b", "d1120304-d6f6-4fd8-a629-4fd9e42fc7a4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "Endeavours",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 12, 29, 10, 14, 11, 87, DateTimeKind.Local).AddTicks(4000), new DateTime(2020, 11, 29, 10, 14, 11, 87, DateTimeKind.Local).AddTicks(3592) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a651393-b1ca-4818-a0cd-f2904b536a1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eb5cc0d-1214-47b1-80b9-42dfa3fc3008");

            migrationBuilder.UpdateData(
                table: "Endeavours",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 12, 29, 9, 9, 17, 776, DateTimeKind.Local).AddTicks(3601), new DateTime(2020, 11, 29, 9, 9, 17, 776, DateTimeKind.Local).AddTicks(2804) });
        }
    }
}
