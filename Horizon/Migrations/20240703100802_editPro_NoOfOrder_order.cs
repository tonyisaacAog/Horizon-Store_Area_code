using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class editPro_NoOfOrder_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NoOfOrder",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 3, 13, 8, 1, 88, DateTimeKind.Local).AddTicks(6474), new DateTime(2024, 7, 3, 13, 8, 1, 88, DateTimeKind.Local).AddTicks(6411) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NoOfOrder",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 3, 12, 34, 10, 440, DateTimeKind.Local).AddTicks(2352), new DateTime(2024, 7, 3, 12, 34, 10, 440, DateTimeKind.Local).AddTicks(2298) });
        }
    }
}
