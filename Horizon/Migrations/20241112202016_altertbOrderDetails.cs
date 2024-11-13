using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class altertbOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetailType",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 12, 22, 20, 14, 639, DateTimeKind.Local).AddTicks(7631), new DateTime(2024, 11, 12, 22, 20, 14, 639, DateTimeKind.Local).AddTicks(7577) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 12, 22, 20, 14, 639, DateTimeKind.Local).AddTicks(8001), new DateTime(2024, 11, 12, 22, 20, 14, 639, DateTimeKind.Local).AddTicks(7996) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 12, 22, 20, 14, 639, DateTimeKind.Local).AddTicks(7950), new DateTime(2024, 11, 12, 22, 20, 14, 639, DateTimeKind.Local).AddTicks(7943) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailType",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 3, 2, 13, 38, 845, DateTimeKind.Local).AddTicks(5069), new DateTime(2024, 11, 3, 2, 13, 38, 845, DateTimeKind.Local).AddTicks(4807) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 3, 2, 13, 38, 845, DateTimeKind.Local).AddTicks(5863), new DateTime(2024, 11, 3, 2, 13, 38, 845, DateTimeKind.Local).AddTicks(5859) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 3, 2, 13, 38, 845, DateTimeKind.Local).AddTicks(5812), new DateTime(2024, 11, 3, 2, 13, 38, 845, DateTimeKind.Local).AddTicks(5790) });
        }
    }
}
