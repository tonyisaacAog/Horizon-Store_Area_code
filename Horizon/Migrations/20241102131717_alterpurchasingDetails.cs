using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterpurchasingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSales",
                table: "Finance_PurchasingModule_PurchasingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Finance_PurchasingModule_PurchasingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 15, 17, 15, 830, DateTimeKind.Local).AddTicks(8530), new DateTime(2024, 11, 2, 15, 17, 15, 830, DateTimeKind.Local).AddTicks(8476) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 15, 17, 15, 830, DateTimeKind.Local).AddTicks(8895), new DateTime(2024, 11, 2, 15, 17, 15, 830, DateTimeKind.Local).AddTicks(8889) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 15, 17, 15, 830, DateTimeKind.Local).AddTicks(8842), new DateTime(2024, 11, 2, 15, 17, 15, 830, DateTimeKind.Local).AddTicks(8832) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "Finance_PurchasingModule_PurchasingDetails");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Finance_PurchasingModule_PurchasingDetails");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(502), new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(800), new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(797) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(769), new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(765) });
        }
    }
}
