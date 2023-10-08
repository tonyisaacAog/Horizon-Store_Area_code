using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addPropertyInOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsManufacturing",
                table: "Finance_OrderModule_OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 15, 17, 15, 37, 325, DateTimeKind.Local).AddTicks(1230), new DateTime(2023, 5, 15, 17, 15, 37, 325, DateTimeKind.Local).AddTicks(1217) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManufacturing",
                table: "Finance_OrderModule_OrderDetails");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 11, 14, 45, 6, 580, DateTimeKind.Local).AddTicks(9035), new DateTime(2023, 5, 11, 14, 45, 6, 580, DateTimeKind.Local).AddTicks(9019) });
        }
    }
}
