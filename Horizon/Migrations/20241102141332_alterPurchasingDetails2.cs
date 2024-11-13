using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterPurchasingDetails2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetailType",
                table: "Finance_PurchasingModule_PurchasingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 16, 13, 30, 565, DateTimeKind.Local).AddTicks(9047), new DateTime(2024, 11, 2, 16, 13, 30, 565, DateTimeKind.Local).AddTicks(8983) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 16, 13, 30, 565, DateTimeKind.Local).AddTicks(9485), new DateTime(2024, 11, 2, 16, 13, 30, 565, DateTimeKind.Local).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 16, 13, 30, 565, DateTimeKind.Local).AddTicks(9361), new DateTime(2024, 11, 2, 16, 13, 30, 565, DateTimeKind.Local).AddTicks(9356) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailType",
                table: "Finance_PurchasingModule_PurchasingDetails");

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
    }
}
