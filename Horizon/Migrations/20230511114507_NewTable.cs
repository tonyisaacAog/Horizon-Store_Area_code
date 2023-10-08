using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class NewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_OrderModule_Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "Date", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_OrderModule_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_OrderModule_Order_Finance_SalesModule_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Finance_SalesModule_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_OrderModule_OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QTY = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ManfactId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_OrderModule_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_OrderModule_OrderDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_OrderModule_OrderDetails_Finance_OrderModule_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Finance_OrderModule_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_OrderModule_OrderConfigure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descrpition = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderDetailsId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_OrderModule_OrderConfigure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_OrderModule_OrderConfigure_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAssets_Store_StoreItemsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_OrderModule_OrderConfigure_Finance_OrderModule_OrderDetails_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "Finance_OrderModule_OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 11, 14, 45, 6, 580, DateTimeKind.Local).AddTicks(9035), new DateTime(2023, 5, 11, 14, 45, 6, 580, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_OrderModule_Order_ClientId",
                table: "Finance_OrderModule_Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_OrderModule_OrderConfigure_OrderDetailsId",
                table: "Finance_OrderModule_OrderConfigure",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_OrderModule_OrderConfigure_StoreItemId",
                table: "Finance_OrderModule_OrderConfigure",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_OrderModule_OrderDetails_OrderId",
                table: "Finance_OrderModule_OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_OrderModule_OrderDetails_ProductId",
                table: "Finance_OrderModule_OrderDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_OrderModule_OrderConfigure");

            migrationBuilder.DropTable(
                name: "Finance_OrderModule_OrderDetails");

            migrationBuilder.DropTable(
                name: "Finance_OrderModule_Order");

            migrationBuilder.UpdateData(
                table: "Finance_CurrentAssets_Store_RawItemType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModified" },
                values: new object[] { new DateTime(2023, 5, 4, 13, 59, 0, 959, DateTimeKind.Local).AddTicks(1930), new DateTime(2023, 5, 4, 13, 59, 0, 959, DateTimeKind.Local).AddTicks(1918) });
        }
    }
}
