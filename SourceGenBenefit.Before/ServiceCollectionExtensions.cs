using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SourceGenBenefit.Before.Create;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBefore(
        this IServiceCollection services,
        bool nullLogger = true,
        bool nullRepository = true)
    {
        services.AddAutoMapper(x => x.AddProfile(new TestEntityMapperProfile()));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<TestEntityMapperProfile>());
        services.AddSingleton<IValidator<CreateTestEntity>, CreateTestEntityValidator>();
        if (nullLogger)
            services.AddLogging(builder => builder.ClearProviders().AddProvider(NullLoggerProvider.Instance));
        if (nullRepository)
            services.AddSingleton<ITestEntityRepository, NullTestEntityRepository>();

        return services;
    }
}