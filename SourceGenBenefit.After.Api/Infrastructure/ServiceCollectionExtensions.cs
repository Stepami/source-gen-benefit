using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After.Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonContext.Default);
        });
        services.AddLogging(builder => builder.ClearProviders().AddConsole());
        var dbSettings = new DbSettings();
        configuration.Bind(dbSettings);
        services.AddNpgsqlSlimDataSource(dbSettings.ConnectionString);
        services.AddSingleton<ITestEntityRepository, TestEntityRepository>();
    }
}