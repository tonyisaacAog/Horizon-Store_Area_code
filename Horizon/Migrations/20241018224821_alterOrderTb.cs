using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterOrderTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvoiceSale",
                table: "Finance_OrderModule_Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2081), new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2007) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2519), new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2457), new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2448) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvoiceSale",
                table: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 13, 0, 18, 41, 325, DateTimeKind.Local).AddTicks(4316), new DateTime(2024, 8, 13, 0, 18, 41, 325, DateTimeKind.Local).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 13, 0, 18, 41, 325, DateTimeKind.Local).AddTicks(4707), new DateTime(2024, 8, 13, 0, 18, 41, 325, DateTimeKind.Local).AddTicks(4704) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 13, 0, 18, 41, 325, DateTimeKind.Local).AddTicks(4591), new DateTime(2024, 8, 13, 0, 18, 41, 325, DateTimeKind.Local).AddTicks(4587) });
        }
    }
}
