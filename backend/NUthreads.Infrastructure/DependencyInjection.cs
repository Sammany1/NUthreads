using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Application.Interfaces.Validators;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Infrastructure.Repositories;
using NUthreads.Infrastructure.Services;

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
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ISignUpValidator, SignUpValidator>();

            return services;
        }
    }
}

