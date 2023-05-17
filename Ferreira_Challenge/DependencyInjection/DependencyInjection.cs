using Microsoft.AspNetCore.DataProtection.Repositories;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration config)
    {
        var mongo_uri = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGO_URI")) ? Environment.GetEnvironmentVariable("MONGO_URI") : config.GetValue<string>("MongoDb:ConnectionString");

        // services.AddSingleton(mongoClient);

        // Add repository classes
        // services.AddScoped<ICarsRepository, CarsRepository>();

        // Add services
        // services.AddScoped<ICarsService, CarsService>();

    }
}