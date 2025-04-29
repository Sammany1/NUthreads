using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Infrastructure.Repositories;

namespace NUthreads.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDbSettings").Bind);

            // Get the configured settings
            var mongoSettings = configuration.GetSection("MongoDbSettings").Get<MongoDBSettings>();
            if (mongoSettings == null)
            {
                throw new InvalidOperationException("MongoDB settings not found in configuration");
            }

            // Register MongoClient as Singleton
            services.AddSingleton<IMongoClient>(_ =>
                new MongoClient(mongoSettings.ConnectionStrings)); // Note: ConnectionStrings (plural)

            services.AddSingleton<NUthreadsDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

