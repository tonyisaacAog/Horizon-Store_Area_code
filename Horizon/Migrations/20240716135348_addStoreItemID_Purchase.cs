using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addStoreItemID_Purchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceItemsRaw",
                table: "Finance_PurchasingModule_Purchasing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StoreItemId",
                table: "Finance_PurchasingModule_Purchasing",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 16, 16, 53, 47, 817, DateTimeKind.Local).AddTicks(3761), new DateTime(2024, 7, 16, 16, 53, 47, 817, DateTimeKind.Local).AddTicks(3716) });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_Purchasing_StoreItemId",
                table: "Finance_PurchasingModule_Purchasing",
                column: "StoreItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_PurchasingModule_Purchasing_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                table: "Finance_PurchasingModule_Purchasing",
                column: "StoreItemId",
                principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_PurchasingModule_Purchasing_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropIndex(
                name: "IX_Finance_PurchasingModule_Purchasing_StoreItemId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropColumn(
                name: "PriceItemsRaw",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropColumn(
                name: "StoreItemId",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 13, 19, 9, 58, 339, DateTimeKind.Local).AddTicks(5655), new DateTime(2024, 7, 13, 19, 9, 58, 339, DateTimeKind.Local).AddTicks(5617) });
        }
    }
}
