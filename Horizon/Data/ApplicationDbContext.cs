using BoldReports.Processing;
using Finance.CurrentAssetModule.Store.Model.Main;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Finance.CurrentAssetModule.Stores.Model.Settings;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.Models.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Manufacturing.Models;
using System.Security.Policy;
using Horizon.Areas.Sales.Models;
using Horizon.Areas.Settings.Models;
using Microsoft.AspNetCore.Identity;
using Horizon.Areas.Orders.Models;

namespace Horizon.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Purchasing>  Purchasings{ get; set; }
        public DbSet<PurchaseOrder>  PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<RawItemType> RawItemTypes{ get; set; }
        public DbSet<StoreLocations> StoreLocations { get; set; }
        public DbSet<StoreMeasureUnit> StoreMeasureUnit { get; set; }
        public DbSet<StoreItemsRaw> StoreItemsRaw { get; set; }
        public DbSet<StoreTransactionsRaw> StoreTransactionsRaw { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<StoreBrand> StoreBrands { get; set; }
        public DbSet<StoreFamily> StoreFamilies { get; set; }
        public DbSet<ItemConfguration> ItemConfgurations { get; set; }
        public DbSet<StoreTransactions> StoreTransactions { get; set; }
        public DbSet<StoreTransactionDetails> StoreTransactionDetails { get; set; }
        public DbSet<StoreLocationsBalance> StoreLocationsBalance { get; set; }
        public DbSet<ManufacturingBatch> ManufacturingBatch { get; set; }
        public DbSet<Client>  Clients { get; set; }
        public DbSet<Sale>  Sales { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderConfigure> OrderConfigures { get; set; }
    }
}