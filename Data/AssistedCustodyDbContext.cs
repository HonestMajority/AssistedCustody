using AssistedCustody.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssistedCustody.Data;

public class AssistedCustodyDbContext : DbContext
{
    public AssistedCustodyDbContext(DbContextOptions<AssistedCustodyDbContext> options)
        : base(options)
    {
    }

    public DbSet<ServerKey> ServerKeys => Set<ServerKey>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServerKey>(entity =>
        {
            entity.ToTable("server_keys");
            entity.HasKey(serverKey => serverKey.Id);
            entity.Property(serverKey => serverKey.XPriv)
                .HasColumnName("xpriv")
                .IsRequired();
        });
    }
}
