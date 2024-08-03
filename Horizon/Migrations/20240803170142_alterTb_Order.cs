using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alterTb_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 3, 20, 1, 41, 473, DateTimeKind.Local).AddTicks(6262), new DateTime(2024, 8, 3, 20, 1, 41, 473, DateTimeKind.Local).AddTicks(6205) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Finance_OrderModule_Order");

            migrationBuilder.DropColumn(
                name: "ClientPhone",
                table: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 2, 4, 48, 16, 363, DateTimeKind.Local).AddTicks(813), new DateTime(2024, 8, 2, 4, 48, 16, 363, DateTimeKind.Local).AddTicks(777) });
        }
    }
}
