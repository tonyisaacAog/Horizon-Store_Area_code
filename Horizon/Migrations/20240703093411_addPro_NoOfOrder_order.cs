using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addPro_NoOfOrder_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoOfOrder",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 3, 12, 34, 10, 440, DateTimeKind.Local).AddTicks(2352), new DateTime(2024, 7, 3, 12, 34, 10, 440, DateTimeKind.Local).AddTicks(2298) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfOrder",
                table: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 2, 22, 12, 26, 325, DateTimeKind.Local).AddTicks(7770), new DateTime(2024, 7, 2, 22, 12, 26, 325, DateTimeKind.Local).AddTicks(7724) });
        }
    }
}
