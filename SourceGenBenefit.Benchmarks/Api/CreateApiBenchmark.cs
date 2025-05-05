using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace SourceGenBenefit.Benchmarks.Api;

[SimpleJob(RuntimeMoniker.Net90)]
public class CreateApiBenchmark
{
    private ServiceProvider _serviceProvider;
    private readonly CreateTestEntityFaker _faker = new();

    private List<CreateTestEntityCommand> _commands = [];

    [Params(10, 100)] public int TestEntitiesCount;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var services = new ServiceCollection();
        services.AddWebApiClient()
            .ConfigureHttpApi(o =>
            {
                o.UseLogging = false;
                o.UseParameterPropertyValidate = false;
                o.UseReturnValuePropertyValidate = false;
            });
        services.AddHttpApi<ITestEntityApiAfter>();
        services.AddHttpApi<ITestEntityApiBefore>();
        _serviceProvider = services.BuildServiceProvider();
        _commands = _faker.Generate(TestEntitiesCount)
            .Select(x => new CreateTestEntityCommand(x))
            .ToList();
    }

    [Benchmark]
    public async Task AfterSourceGen()
    {
        using var scope = _serviceProvider.CreateScope();
        var after = scope.ServiceProvider.GetService<ITestEntityApiAfter>();
        for (var i = 0; i < TestEntitiesCount; i++)
        {
            await after.Create(_commands[i]);
        }
    }

    [Benchmark]
    public async Task BeforeSourceGen()
    {
        using var scope = _serviceProvider.CreateScope();
        var before = scope.ServiceProvider.GetService<ITestEntityApiBefore>();
        for (var i = 0; i < TestEntitiesCount; i++)
        {
            await before.Create(_commands[i]);
        }
    }

    [GlobalCleanup]
    public async Task GlobalCleanup()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            await scope.ServiceProvider.GetRequiredService<ITestEntityApiBefore>().Clear();
            await scope.ServiceProvider.GetRequiredService<ITestEntityApiAfter>().Clear();
        }

        await _serviceProvider.DisposeAsync();
        _commands.Clear();
    }
}