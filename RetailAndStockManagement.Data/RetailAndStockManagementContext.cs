using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Data.EF;
using System.Diagnostics.Metrics;

namespace RetailAndStockManagement.Data.EF;

public class RetailAndStockManagementContext : DbContext, IRetailAndStockManagementContext
{
    public RetailAndStockManagementContext(DbContextOptions<RetailAndStockManagementContext> options)
        : base(options) { }

    public DbSet<CustomerModel> Customers => Set<CustomerModel>();
    public DbSet<ProductModel> Products => Set<ProductModel>();
    public DbSet<StoreModel> Stores => Set<StoreModel>();
    public DbSet<ProductStoreModel> ProductStores => Set<ProductStoreModel>();
    public DbSet<SaleModel> Sales => Set<SaleModel>();
    public DbSet<CountryModel> Countries => Set<CountryModel>();
    public DbSet<RegionModel> Regions => Set<RegionModel>();
    public DbSet<CartModel> Carts => Set<CartModel>();
    public DbSet<CartItemModel> CartItems => Set<CartItemModel>();


    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
}
