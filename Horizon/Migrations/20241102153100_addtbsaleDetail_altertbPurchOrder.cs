using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addtbsaleDetail_altertbPurchOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Finance_PurchasingModule_SaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailType = table.Column<int>(type: "int", nullable: false),
                    StoreItemId = table.Column<int>(type: "int", nullable: true),
                    StoreItemsRawId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_PurchasingModule_SaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_SaleDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_SaleDetails_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemsRawId",
                        column: x => x.StoreItemsRawId,
                        principalTable: "Finance_CurrentAssets_Store_StoreItemsRaw",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_SaleDetails_Finance_SalesModule_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Finance_SalesModule_Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchaseOrderDetails_StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                column: "StoreItemsRawId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_SaleDetails_SaleId",
                table: "Finance_PurchasingModule_SaleDetails",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_SaleDetails_StoreItemId",
                table: "Finance_PurchasingModule_SaleDetails",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_SaleDetails_StoreItemsRawId",
                table: "Finance_PurchasingModule_SaleDetails",
                column: "StoreItemsRawId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                column: "StoreItemId",
                principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                column: "StoreItemsRawId",
                principalTable: "Finance_CurrentAssets_Store_StoreItemsRaw",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "Finance_PurchasingModule_SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_Finance_PurchasingModule_PurchaseOrderDetails_StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_PurchasingModule_PurchaseOrderDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                table: "Finance_PurchasingModule_PurchaseOrderDetails",
                column: "StoreItemId",
                principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
