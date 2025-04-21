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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.AtlasURI);
            });


            services.AddDbContext<NUthreadsDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<INewUserDTOValidator, NewUserDTOValidator>();


            return services;
        }
    }
}
