using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before.Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(builder => builder.ClearProviders().AddConsole());
        var dbSettings = new DbSettings();
        configuration.Bind(dbSettings);
        services.AddNpgsqlDataSource(dbSettings.ConnectionString);
        services.AddSingleton<ITestEntityRepository, TestEntityRepository>();
    }
}