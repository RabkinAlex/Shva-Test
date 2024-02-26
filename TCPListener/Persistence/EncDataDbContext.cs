using Microsoft.EntityFrameworkCore;
using TCPListener.Persistence.Entities;

namespace TCPListener.Persistence;

public class EncDataDbContext : DbContext
{
    public DbSet<TestTableEntity> TestTable { get; set; }

    public EncDataDbContext(DbContextOptions<EncDataDbContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TeatTableEntityConfiguration());
    }
}
