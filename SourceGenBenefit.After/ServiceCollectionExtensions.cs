using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAfter(
        this IServiceCollection services,
        bool nullLogger = true,
        bool nullRepository = true)
    {
        services.AddMediator();
        if (nullLogger)
            services.AddLogging(builder => builder.ClearProviders().AddProvider(NullLoggerProvider.Instance));
        if (nullRepository)
            services.AddSingleton<ITestEntityRepository, NullTestEntityRepository>();
        return services;
    }
}