using Microsoft.AspNetCore.DataProtection.Repositories;
using Services;
using Repository;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration config)
    {
        var mongo_uri = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGO_URI")) ? Environment.GetEnvironmentVariable("MONGO_URI") : config.GetValue<string>("MongoDb:ConnectionString");

        // services.AddSingleton(mongoClient);

        // Add repository classes
        services.AddScoped<IUserRepository, UserRepository>();

        // Add services
        services.AddScoped<IUserService, UserService>();

    }
}