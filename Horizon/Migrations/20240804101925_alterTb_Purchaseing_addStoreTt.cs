using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterTb_Purchaseing_addStoreTt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountStoreItem",
                table: "Finance_PurchasingModule_Purchasing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 4, 13, 19, 24, 763, DateTimeKind.Local).AddTicks(3406), new DateTime(2024, 8, 4, 13, 19, 24, 763, DateTimeKind.Local).AddTicks(3363) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountStoreItem",
                table: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 4, 12, 31, 20, 399, DateTimeKind.Local).AddTicks(627), new DateTime(2024, 8, 4, 12, 31, 20, 399, DateTimeKind.Local).AddTicks(577) });
        }
    }
}
