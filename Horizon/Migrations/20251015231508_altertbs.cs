using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class altertbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Finance_SalesModule_Sale",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2025, 10, 16, 2, 15, 6, 434, DateTimeKind.Local).AddTicks(1925), new DateTime(2025, 10, 16, 2, 15, 6, 434, DateTimeKind.Local).AddTicks(1854) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2025, 10, 16, 2, 15, 6, 434, DateTimeKind.Local).AddTicks(2382), new DateTime(2025, 10, 16, 2, 15, 6, 434, DateTimeKind.Local).AddTicks(2377) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2025, 10, 16, 2, 15, 6, 434, DateTimeKind.Local).AddTicks(2324), new DateTime(2025, 10, 16, 2, 15, 6, 434, DateTimeKind.Local).AddTicks(2316) });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_SalesModule_Sale_OrderId",
                table: "Finance_SalesModule_Sale",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_SalesModule_Sale_Finance_OrderModule_Order_OrderId",
                table: "Finance_SalesModule_Sale",
                column: "OrderId",
                principalTable: "Finance_OrderModule_Order",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_SalesModule_Sale_Finance_OrderModule_Order_OrderId",
                table: "Finance_SalesModule_Sale");

            migrationBuilder.DropIndex(
                name: "IX_Finance_SalesModule_Sale_OrderId",
                table: "Finance_SalesModule_Sale");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Finance_SalesModule_Sale");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2025, 9, 17, 19, 5, 8, 401, DateTimeKind.Local).AddTicks(7040), new DateTime(2025, 9, 17, 19, 5, 8, 401, DateTimeKind.Local).AddTicks(7002) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2025, 9, 17, 19, 5, 8, 401, DateTimeKind.Local).AddTicks(7306), new DateTime(2025, 9, 17, 19, 5, 8, 401, DateTimeKind.Local).AddTicks(7303) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2025, 9, 17, 19, 5, 8, 401, DateTimeKind.Local).AddTicks(7226), new DateTime(2025, 9, 17, 19, 5, 8, 401, DateTimeKind.Local).AddTicks(7222) });
        }
    }
}
