using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class updateOrderConfigureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Finance_OrderModule_OrderConfigure",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 17, 15, 30, 4, 149, DateTimeKind.Local).AddTicks(1419), new DateTime(2023, 5, 17, 15, 30, 4, 149, DateTimeKind.Local).AddTicks(1407) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Finance_OrderModule_OrderConfigure");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 17, 13, 25, 42, 118, DateTimeKind.Local).AddTicks(1572), new DateTime(2023, 5, 17, 13, 25, 42, 118, DateTimeKind.Local).AddTicks(1553) });
        }
    }
}
