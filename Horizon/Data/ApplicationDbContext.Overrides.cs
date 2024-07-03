using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Orders.Models;
using Horizon.Areas.Store.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Horizon.Data
{
    public partial class ApplicationDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }

            foreach (var type in GetEntityTypesForString())
            {
                var method = SetGlobalQueryMethodForString.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }

            base.OnModelCreating(builder);
            builder.Entity<ItemConfguration>().HasKey(x => new { x.StoreItemId, x.StoreItemRawId });
            builder.Entity<StoreLocationsBalance>().HasKey(x => new { x.LocationId, x.StoreItemId });
            builder.Entity<RawItemType>().HasData(
                    new RawItemType()
                    {
                        Id = 1,
                        RawItemTypeName = "صاج",
                        LastModified = DateTime.Now,
                        DateCreated = DateTime.Now,
                        ModifiedBy = "",
                        CreatedBy = ""
                    }) ;
        }
        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        public void SetGlobalQueryForString<T>(ModelBuilder builder) where T : BaseEntityStringKey
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        static readonly MethodInfo SetGlobalQueryMethod =
        typeof(ApplicationDbContext)
        .GetMethods(BindingFlags.Public |
            BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        static readonly MethodInfo SetGlobalQueryMethodForString =
         typeof(ApplicationDbContext)
         .GetMethods(BindingFlags.Public |
             BindingFlags.Instance)
         .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForString");


        private static IList<Type> _entityTypeCache;
        private static IList<Type> _entityTypeCacheString;
        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseEntity)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IList<Type> GetEntityTypesForString()
        {
            if (_entityTypeCacheString != null)
            {
                return _entityTypeCacheString.ToList();
            }

            _entityTypeCacheString = (from a in GetReferencingAssemblies()
                                      from t in a.DefinedTypes
                                      where t.BaseType == typeof(BaseEntityStringKey)
                                      select t.AsType()).ToList();

            return _entityTypeCacheString;
        }
        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }
        private string GetLoggedInEmployeeUserName()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContext.User != null)
                {
                    var user = httpContext.User.Identity;
                    if (user != null)
                    {
                        var userIdStr = user.Name;
                        return userIdStr;
                    }
                }
            }
            return null;
        }
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    var now = DateTime.UtcNow;
                    var userName = GetLoggedInEmployeeUserName();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastModified = now;
                            trackable.ModifiedBy = userName;
                            break;

                        case EntityState.Added:
                            trackable.DateCreated = now;
                            trackable.LastModified = now;
                            trackable.IsDeleted = false;
                            trackable.ModifiedBy = userName;
                            trackable.CreatedBy = userName;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.ModifiedBy = userName;
                            trackable.LastModified = now;
                            trackable.IsDeleted = true;
                            break;
                    }
                }

                if (entry.Entity is BaseEntityStringKey trackableString)
                {
                    var now = DateTime.UtcNow;
                    var userId = GetLoggedInEmployeeUserName();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackableString.LastModified = now;
                            trackableString.ModifiedBy = userId;
                            break;

                        case EntityState.Added:
                            trackableString.DateCreated = now;
                            trackableString.LastModified = now;
                            trackableString.IsDeleted = false;
                            trackableString.ModifiedBy = userId;
                            trackableString.CreatedBy = userId;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackableString.ModifiedBy = userId;
                            trackableString.LastModified = now;
                            trackableString.IsDeleted = true;
                            break;
                    }
                }
            }
        }
        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
