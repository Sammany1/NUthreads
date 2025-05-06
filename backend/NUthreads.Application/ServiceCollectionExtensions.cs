using Microsoft.Extensions.DependencyInjection;

namespace NUthreads.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(
                typeof(ServiceCollectionExtensions).Assembly));
        
        return service;
    }
}
