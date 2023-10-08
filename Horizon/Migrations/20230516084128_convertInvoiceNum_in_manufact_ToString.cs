using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class convertInvoiceNum_in_manufact_ToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BatchNumber",
                table: "Finance_Manufacturing_ManufacturingBatch",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 16, 11, 41, 27, 944, DateTimeKind.Local).AddTicks(535), new DateTime(2023, 5, 16, 11, 41, 27, 944, DateTimeKind.Local).AddTicks(519) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BatchNumber",
                table: "Finance_Manufacturing_ManufacturingBatch",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 15, 17, 15, 37, 325, DateTimeKind.Local).AddTicks(1230), new DateTime(2023, 5, 15, 17, 15, 37, 325, DateTimeKind.Local).AddTicks(1217) });
        }
    }
}
