using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class updateStoreItemRawTrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DestroyQty",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RestQty",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isEmpty",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 4, 13, 59, 0, 959, DateTimeKind.Local).AddTicks(1930), new DateTime(2023, 5, 4, 13, 59, 0, 959, DateTimeKind.Local).AddTicks(1918) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestroyQty",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw");

            migrationBuilder.DropColumn(
                name: "RestQty",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw");

            migrationBuilder.DropColumn(
                name: "isEmpty",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 4, 30, 0, 33, 30, 899, DateTimeKind.Local).AddTicks(5909), new DateTime(2023, 4, 30, 0, 33, 30, 899, DateTimeKind.Local).AddTicks(5895) });
        }
    }
}
