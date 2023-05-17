using Microsoft.AspNetCore.DataProtection.Repositories;
using Services;
using Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ConnectionString");

        // Configure EF Core with Oracle
        services.AddDbContext<MyDbContext>(options =>
            options.UseOracle(connectionString));

        // Add repository classes
        services.AddScoped<IUserRepository, UserRepository>();

        // Add services
        services.AddScoped<IUserService, UserService>();

    }
}