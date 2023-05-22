using Services;
using Repository;
using Microsoft.EntityFrameworkCore;
using Services.Auth;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config["ConnectionStrings:DefaultConnection"];

        // Configure EF Core with Oracle
        services.AddDbContext<MyDbContext>(options =>
            options.UseOracle(connectionString));

        // Add repository classes
        services.AddScoped<IUserRepository, UserRepository>();

        // Add services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

    }
}