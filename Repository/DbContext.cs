using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

public class MyDbContext : DbContext
{
    private readonly IConfiguration configuration;
    public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration) : base(options)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseOracle(connectionString);
    }

    // DbSet properties representing your database tables
    public DbSet<User> Users { get; set; }

}