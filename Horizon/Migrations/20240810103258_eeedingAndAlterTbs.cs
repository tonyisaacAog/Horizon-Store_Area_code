using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class eeedingAndAlterTbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountStoreItem",
                table: "Finance_PurchasingModule_Purchasing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Finance_PurchasingModule_Purchasing",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreatedASPurchasing",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3555), new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3510) });

            migrationBuilder.InsertData(
                table: "Finance_PurchasingModule_Supplier",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "Email", "IsDeleted", "LastModified", "ModifiedBy", "Phone1", "Phone2", "Phone3", "SupplierName", "SupplierNameAr" },
                values: new object[] { 1, "", new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3867), "", false, new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3864), "", "", "", "", "مورد عام", "مورد عام" });

            migrationBuilder.InsertData(
                table: "Finance_SalesModule_Client",
                columns: new[] { "Id", "ClientName", "ClientNameAr", "CreatedBy", "DateCreated", "Email", "IsDeleted", "LastModified", "ModifiedBy", "Phone1", "Phone2", "Phone3" },
                values: new object[] { 1, "عميل عام", "عميل عام", "", new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3832), "", false, new DateTime(2024, 8, 10, 13, 32, 58, 31, DateTimeKind.Local).AddTicks(3828), "", "", "", "" });

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

            migrationBuilder.DeleteData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AmountStoreItem",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropColumn(
                name: "IsCreatedASPurchasing",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 3, 20, 1, 41, 473, DateTimeKind.Local).AddTicks(6262), new DateTime(2024, 8, 3, 20, 1, 41, 473, DateTimeKind.Local).AddTicks(6205) });
        }
    }
}
