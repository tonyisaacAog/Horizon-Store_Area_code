using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterTb_Purchasing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 4, 12, 31, 20, 399, DateTimeKind.Local).AddTicks(627), new DateTime(2024, 8, 4, 12, 31, 20, 399, DateTimeKind.Local).AddTicks(577) });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_Purchasing_PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_PurchasingModule_Purchasing_Finance_PurchasingModule_PurchaseOrder_PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing",
                column: "PurchaseOrderId",
                principalTable: "Finance_PurchasingModule_PurchaseOrder",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_PurchasingModule_Purchasing_Finance_PurchasingModule_PurchaseOrder_PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropIndex(
                name: "IX_Finance_PurchasingModule_Purchasing_PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 3, 20, 1, 41, 473, DateTimeKind.Local).AddTicks(6262), new DateTime(2024, 8, 3, 20, 1, 41, 473, DateTimeKind.Local).AddTicks(6205) });
        }
    }
}
