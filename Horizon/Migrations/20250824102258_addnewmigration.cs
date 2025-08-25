using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addnewmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Finance_OrderModule_OrderDetails_ManfactId",
                table: "Finance_OrderModule_OrderDetails",
                column: "ManfactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_OrderModule_OrderDetails_Finance_Manufacturing_ManufacturingBatch_ManfactId",
                table: "Finance_OrderModule_OrderDetails",
                column: "ManfactId",
                principalTable: "Finance_Manufacturing_ManufacturingBatch",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_OrderModule_OrderDetails_Finance_Manufacturing_ManufacturingBatch_ManfactId",
                table: "Finance_OrderModule_OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Finance_OrderModule_OrderDetails_ManfactId",
                table: "Finance_OrderModule_OrderDetails");

        }
    }
}
