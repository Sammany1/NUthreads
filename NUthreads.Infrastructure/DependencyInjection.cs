using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Infrastructure.Repositories;
using System.Security.Authentication;

namespace NUthreads.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.AtlasURI);
            });

            
            services.AddSingleton<NUthreadsMongoDbContext>();

            
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
