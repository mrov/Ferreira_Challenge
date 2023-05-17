using Models;
using System.Data.Entity;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Configure your entity mappings here if needed
        base.OnModelCreating(modelBuilder);
    }
}