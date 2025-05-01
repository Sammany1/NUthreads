using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Infrastructure.Repositories;

namespace NUthreads.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IOptions<MongoDBSettings> settings)
        {
            var connectionString = settings.Value.ConnectionString;
            var dbName = settings.Value.DatabaseName;
            services.AddDbContext<NUthreadsDbContext>(options =>
            {
                options.UseMongoDB(connectionString, dbName);
            });

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

