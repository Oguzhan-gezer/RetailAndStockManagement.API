using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Data.EF;

public class RetailAndStockManagementContext : DbContext, IRetailAndStockManagementContext
{
    public RetailAndStockManagementContext(DbContextOptions<RetailAndStockManagementContext> options)
        : base(options) { }

    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<ProductModel> Products => Set<ProductModel>();
    public DbSet<StoreModel> Stores => Set<StoreModel>();
    public DbSet<ProductStoreModel> ProductStores => Set<ProductStoreModel>();
    public DbSet<CountryModel> Countries => Set<CountryModel>();
    public DbSet<RegionModel> Regions => Set<RegionModel>();
    public DbSet<TransferRequestModel> TransferRequests => Set<TransferRequestModel>();
    public DbSet<TransferOrderModel> TransferOrders => Set<TransferOrderModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User - unique username index
        modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // User - Store relationship (optional)
        modelBuilder.Entity<UserModel>()
            .HasOne(u => u.Store)
            .WithMany()
            .HasForeignKey(u => u.StoreId)
            .OnDelete(DeleteBehavior.SetNull);

        // TransferRequest relationships
        modelBuilder.Entity<TransferRequestModel>()
            .HasOne(tr => tr.Product)
            .WithMany()
            .HasForeignKey(tr => tr.Barcode)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferRequestModel>()
            .HasOne(tr => tr.SourceStore)
            .WithMany()
            .HasForeignKey(tr => tr.SourceStoreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferRequestModel>()
            .HasOne(tr => tr.CreatedByUser)
            .WithMany()
            .HasForeignKey(tr => tr.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // TransferOrder relationships
        modelBuilder.Entity<TransferOrderModel>()
            .HasOne(to => to.TransferRequest)
            .WithMany(tr => tr.TransferOrders)
            .HasForeignKey(to => to.TransferRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferOrderModel>()
            .HasOne(to => to.TargetStore)
            .WithMany()
            .HasForeignKey(to => to.TargetStoreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferOrderModel>()
            .HasOne(to => to.CreatedByUser)
            .WithMany()
            .HasForeignKey(to => to.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
}
