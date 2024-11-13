using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterstoreTransationItemRaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 17, 30, 55, 330, DateTimeKind.Local).AddTicks(7451), new DateTime(2024, 11, 2, 17, 30, 55, 330, DateTimeKind.Local).AddTicks(7352) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 17, 30, 55, 330, DateTimeKind.Local).AddTicks(7884), new DateTime(2024, 11, 2, 17, 30, 55, 330, DateTimeKind.Local).AddTicks(7877) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 17, 30, 55, 330, DateTimeKind.Local).AddTicks(7765), new DateTime(2024, 11, 2, 17, 30, 55, 330, DateTimeKind.Local).AddTicks(7757) });
        }
    }
}
