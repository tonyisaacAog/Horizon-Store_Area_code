using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class altertb_purchaseorder_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Finance_PurchasingModule_PurchaseOrder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Finance_PurchasingModule_PurchaseOrder",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Finance_PurchasingModule_PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Finance_PurchasingModule_PurchaseOrder");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3555), new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3867), new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3864) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3832), new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3828) });
        }
    }
}
