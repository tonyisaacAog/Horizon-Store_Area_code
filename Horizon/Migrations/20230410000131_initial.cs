using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Stores_Settings_StoreBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreBrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StoreBrandNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Stores_Settings_StoreBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Stores_Settings_StoreFamily",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Stores_Settings_StoreFamily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_RawItemType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawItemTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_RawItemType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_StoreTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    DestinationType = table.Column<int>(type: "int", nullable: false),
                    TransDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    Descrpition = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StoreLocationId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_StoreTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_StoreSetting_StoreLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefaultLocation = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_StoreSetting_StoreLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_StoreSetting_StoreMeasureUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasureUnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ECode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_StoreSetting_StoreMeasureUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Manufacturing_ManufacturingBatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchDate = table.Column<DateTime>(type: "Date", nullable: false),
                    BatchNumber = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Manufacturing_ManufacturingBatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_PurchasingModule_Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupplierNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_PurchasingModule_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_SalesModule_Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClientNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_SalesModule_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductNameAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false),
                    StoreBrandId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrentQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DestroyedQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Stores_Settings_StoreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Stores_Settings_StoreItem_Finance_CurrentAsset_Stores_Settings_StoreBrand_StoreBrandId",
                        column: x => x.StoreBrandId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Stores_Settings_StoreItem_Finance_CurrentAsset_Stores_Settings_StoreFamily_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreFamily",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_StoreItemsRaw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ItemNameAr = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    RawItemTypeId = table.Column<int>(type: "int", nullable: false),
                    QTY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarningLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_StoreItemsRaw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_StoreItemsRaw_Finance_CurrentAssets_Store_RawItemType_RawItemTypeId",
                        column: x => x.RawItemTypeId,
                        principalTable: "Finance_CurrentAssets_Store_RawItemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_PurchasingModule_Purchasing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchasingDate = table.Column<DateTime>(type: "Date", nullable: true),
                    InvoiceNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_PurchasingModule_Purchasing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_PurchasingModule_Purchasing_Finance_PurchasingModule_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Finance_PurchasingModule_Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_SalesModule_Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesDate = table.Column<DateTime>(type: "Date", nullable: true),
                    InvoiceNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_SalesModule_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_SalesModule_Sale_Finance_SalesModule_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Finance_SalesModule_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_StoreLocationsBalance",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    DestroyedQty = table.Column<int>(type: "int", nullable: false),
                    QTY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_StoreLocationsBalance", x => new { x.LocationId, x.StoreItemId });
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_StoreLocationsBalance_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_StoreLocationsBalance_Finance_CurrentAssets_StoreSetting_StoreLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Finance_CurrentAssets_StoreSetting_StoreLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_StoreTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreTransId = table.Column<int>(type: "int", nullable: false),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    QTY = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtyBalanceAfterSource = table.Column<int>(type: "int", nullable: true),
                    QtyBalanceAfterDestination = table.Column<int>(type: "int", nullable: true),
                    AmountBalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ManfactId = table.Column<int>(type: "int", nullable: true),
                    ManfactUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_StoreTransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_StoreTransactionDetails_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_StoreTransactionDetails_Finance_CurrentAssets_Store_StoreTransactions_StoreTransId",
                        column: x => x.StoreTransId,
                        principalTable: "Finance_CurrentAssets_Store_StoreTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_ItemConfguration",
                columns: table => new
                {
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    StoreItemRawId = table.Column<int>(type: "int", nullable: false),
                    MinimumAmount = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_ItemConfguration", x => new { x.StoreItemId, x.StoreItemRawId });
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_ItemConfguration_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_ItemConfguration_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemRawId",
                        column: x => x.StoreItemRawId,
                        principalTable: "Finance_CurrentAssets_Store_StoreItemsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    QtyBalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountBalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransDate = table.Column<DateTime>(type: "Date", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Descrpition = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    ManfacturingId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAssets_Store_StoreTransactionsRaw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAssets_Store_StoreTransactionsRaw_Finance_CurrentAssets_Store_StoreItemsRaw_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAssets_Store_StoreItemsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Stores_Settings_StoreItem_FamilyId",
                table: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Stores_Settings_StoreItem_StoreBrandId",
                table: "Finance_CurrentAsset_Stores_Settings_StoreItem",
                column: "StoreBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAssets_Store_ItemConfguration_StoreItemRawId",
                table: "Finance_CurrentAssets_Store_ItemConfguration",
                column: "StoreItemRawId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAssets_Store_StoreItemsRaw_RawItemTypeId",
                table: "Finance_CurrentAssets_Store_StoreItemsRaw",
                column: "RawItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAssets_Store_StoreLocationsBalance_StoreItemId",
                table: "Finance_CurrentAssets_Store_StoreLocationsBalance",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAssets_Store_StoreTransactionDetails_StoreItemId",
                table: "Finance_CurrentAssets_Store_StoreTransactionDetails",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAssets_Store_StoreTransactionDetails_StoreTransId",
                table: "Finance_CurrentAssets_Store_StoreTransactionDetails",
                column: "StoreTransId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAssets_Store_StoreTransactionsRaw_StoreItemId",
                table: "Finance_CurrentAssets_Store_StoreTransactionsRaw",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_PurchasingModule_Purchasing_SupplierId",
                table: "Finance_PurchasingModule_Purchasing",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_SalesModule_Sale_ClientId",
                table: "Finance_SalesModule_Sale",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_ItemConfguration");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_StoreLocationsBalance");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_StoreTransactionDetails");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_StoreTransactionsRaw");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_StoreSetting_StoreMeasureUnit");

            migrationBuilder.DropTable(
                name: "Finance_Manufacturing_ManufacturingBatch");

            migrationBuilder.DropTable(
                name: "Finance_PurchasingModule_Purchasing");

            migrationBuilder.DropTable(
                name: "Finance_SalesModule_Sale");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_StoreSetting_StoreLocations");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Stores_Settings_StoreItem");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_StoreTransactions");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_StoreItemsRaw");

            migrationBuilder.DropTable(
                name: "Finance_PurchasingModule_Supplier");

            migrationBuilder.DropTable(
                name: "Finance_SalesModule_Client");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Stores_Settings_StoreBrand");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Stores_Settings_StoreFamily");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAssets_Store_RawItemType");
        }
    }
}
