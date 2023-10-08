using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class updateorderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcess",
                table: "Finance_OrderModule_Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 17, 13, 25, 42, 118, DateTimeKind.Local).AddTicks(1572), new DateTime(2023, 5, 17, 13, 25, 42, 118, DateTimeKind.Local).AddTicks(1553) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcess",
                table: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 16, 11, 41, 27, 944, DateTimeKind.Local).AddTicks(535), new DateTime(2023, 5, 16, 11, 41, 27, 944, DateTimeKind.Local).AddTicks(519) });
        }
    }
}
