using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class addtbPurchasingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_PurchasingModule_PurchasingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreItemId = table.Column<int>(type: "int", nullable: true),
                    StoreItemsRawId = table.Column<int>(type: "int", nullable: true),
                    PurchasingId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_PurchasingModule_PurchasingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_PurchasingDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_PurchasingDetails_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemsRawId",
                        column: x => x.StoreItemsRawId,
                        principalTable: "Finance_CurrentAssets_Store_StoreItemsRaw",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_PurchasingDetails_Finance_PurchasingModule_Purchasing_PurchasingId",
                        column: x => x.PurchasingId,
                        principalTable: "Finance_PurchasingModule_Purchasing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(502), new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(800), new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(797) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(769), new DateTime(2024, 11, 2, 13, 48, 56, 458, DateTimeKind.Local).AddTicks(765) });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchasingDetails_PurchasingId",
                table: "Finance_PurchasingModule_PurchasingDetails",
                column: "PurchasingId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchasingDetails_StoreItemId",
                table: "Finance_PurchasingModule_PurchasingDetails",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_PurchasingDetails_StoreItemsRawId",
                table: "Finance_PurchasingModule_PurchasingDetails",
                column: "StoreItemsRawId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_PurchasingModule_PurchasingDetails");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2081), new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2007) });

            migrationBuilder.UpdateData(
                table: "Finance_PurchasingModule_Supplier",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2519), new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "Finance_SalesModule_Client",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2457), new DateTime(2024, 10, 19, 1, 48, 19, 582, DateTimeKind.Local).AddTicks(2448) });
        }
    }
}
