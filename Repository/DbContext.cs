using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    // DbSet properties representing your database tables
    public DbSet<User> Users { get; set; }

}