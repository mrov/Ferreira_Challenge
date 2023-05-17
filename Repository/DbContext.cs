using Microsoft.EntityFrameworkCore;
using Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=FREE)));User Id=SYS;Password=password;DBA Privilege=SYSDBA;");
    }

    // DbSet properties representing your database tables
    public DbSet<User> Users { get; set; }

    // ...
}