using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace NUthreads.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));
        
        return service;
    }
}
