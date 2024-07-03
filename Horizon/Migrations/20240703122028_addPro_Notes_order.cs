using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addPro_Notes_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Finance_OrderModule_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 3, 15, 20, 27, 549, DateTimeKind.Local).AddTicks(4497), new DateTime(2024, 7, 3, 15, 20, 27, 549, DateTimeKind.Local).AddTicks(4450) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 3, 13, 8, 1, 88, DateTimeKind.Local).AddTicks(6474), new DateTime(2024, 7, 3, 13, 8, 1, 88, DateTimeKind.Local).AddTicks(6411) });
        }
    }
}
