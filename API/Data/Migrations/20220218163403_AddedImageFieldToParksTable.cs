using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AddedImageFieldToParksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Parks",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Parks");

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "Id", "Created", "Established", "Name", "State" },
                values: new object[] { 1, new DateTime(2022, 2, 16, 22, 36, 48, 861, DateTimeKind.Local).AddTicks(3100), new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Park 1", "Shebin" });

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "Id", "Created", "Established", "Name", "State" },
                values: new object[] { 2, new DateTime(2022, 2, 9, 22, 36, 48, 861, DateTimeKind.Local).AddTicks(3112), new DateTime(2002, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Park 2", "Sadat" });
        }
    }
}
