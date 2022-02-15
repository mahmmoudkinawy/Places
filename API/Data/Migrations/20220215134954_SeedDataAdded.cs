using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class SeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "Id", "Created", "Established", "Name", "State" },
                values: new object[] { 1, new DateTime(2022, 2, 15, 15, 49, 54, 692, DateTimeKind.Local).AddTicks(3197), new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Park 1", "Shebin" });

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "Id", "Created", "Established", "Name", "State" },
                values: new object[] { 2, new DateTime(2022, 2, 8, 15, 49, 54, 692, DateTimeKind.Local).AddTicks(3210), new DateTime(2002, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Park 2", "Sadat" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
