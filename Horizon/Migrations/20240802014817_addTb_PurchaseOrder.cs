using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addTb_PurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_PurchasingModule_PurchaseOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStoreInStock = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsStoreInStock = table.Column<bool>(type: "bit", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_PurchasingModule_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_PurchaseOrder_Finance_PurchasingModule_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Finance_PurchasingModule_Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_PurchasingModule_PurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_PurchasingModule_PurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_PurchasingModule_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Finance_PurchasingModule_PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 8, 2, 4, 48, 16, 363, DateTimeKind.Local).AddTicks(813), new DateTime(2024, 8, 2, 4, 48, 16, 363, DateTimeKind.Local).AddTicks(777) });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchaseOrder_SupplierId",
                table: "Finance_PurchasingModule_PurchaseOrder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchaseOrderDetails_PurchaseOrderId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchaseOrderDetails_StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                column: "StoreItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "Finance_PurchasingModule_PurchaseOrder");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 7, 16, 16, 53, 47, 817, DateTimeKind.Local).AddTicks(3761), new DateTime(2024, 7, 16, 16, 53, 47, 817, DateTimeKind.Local).AddTicks(3716) });
        }
    }
}
