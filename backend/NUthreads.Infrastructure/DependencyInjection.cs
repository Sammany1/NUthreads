using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Application.Interfaces.Utilities;
using NUthreads.Application.Interfaces.Validators;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Infrastructure.Repositories;
using NUthreads.Infrastructure.Services;
using NUthreads.Infrastructure.Utilities;

namespace NUthreads.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IOptions<MongoDBSettings> settings)
        {
            var connectionString = settings.Value.ConnectionString ?? throw new ArgumentNullException("Connection String Is Null"); ;
            var dbName = settings.Value.DatabaseName ?? throw new ArgumentNullException("Database Name Is Null"); ;

            services.AddDbContext<NUthreadsDbContext>(options =>
            {
                options.UseMongoDB(connectionString, dbName);
            });
            services.AddScoped<IRevokedTokenRepository, RevokedTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IRegisterValidator, RegisterValidator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();




            return services;
        }
    }
}

