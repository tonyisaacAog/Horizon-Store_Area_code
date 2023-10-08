using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class newColInStoreTransRaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalWithrd",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DestroyedQty",
                table: "Finance_CurrentAssets_Store_StoreItemsRaw",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "IsDeleted", "LastModified", "ModifiedBy", "RawItemTypeName" },
                values: new object[] { 1, "", new DateTime(2023, 4, 30, 0, 33, 30, 899, DateTimeKind.Local).AddTicks(5909), false, new DateTime(2023, 4, 30, 0, 33, 30, 899, DateTimeKind.Local).AddTicks(5895), "", "صاج" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "TotalWithrd",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw");

            migrationBuilder.DropColumn(
                name: "DestroyedQty",
                table: "Finance_CurrentAssets_Store_StoreItemsRaw");
        }
    }
}
