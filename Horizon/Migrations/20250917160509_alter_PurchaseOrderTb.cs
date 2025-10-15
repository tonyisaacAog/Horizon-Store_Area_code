using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class alter_PurchaseOrderTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReceivedAmount",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedAmount",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

        }
    }
}
