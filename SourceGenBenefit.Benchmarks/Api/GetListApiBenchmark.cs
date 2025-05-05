using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace SourceGenBenefit.Benchmarks.Api;

[SimpleJob(RuntimeMoniker.Net90)]
public class GetListApiBenchmark
{
    private ServiceProvider _serviceProvider;
    private readonly CreateTestEntityFaker _faker = new();
    private readonly Consumer _consumer = new();

    [Params(10, 100)] public int QueryCount;

    [GlobalSetup]
    public async Task GlobalSetup()
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
        using var scope = _serviceProvider.CreateScope();
        var before = scope.ServiceProvider.GetRequiredService<ITestEntityApiBefore>();
        var after = scope.ServiceProvider.GetRequiredService<ITestEntityApiAfter>();
        foreach (var command in _faker.Generate(10).Select(x => new CreateTestEntityCommand(x)))
        {
            await before.Create(command);
            await after.Create(command);
        }
    }

    [Benchmark]
    public async Task AfterSourceGen()
    {
        using var scope = _serviceProvider.CreateScope();
        var after = scope.ServiceProvider.GetService<ITestEntityApiAfter>();
        for (var i = 0; i < QueryCount; i++)
        {
            var list = await after.GetList();
            _consumer.Consume(list);
        }
    }

    [Benchmark]
    public async Task BeforeSourceGen()
    {
        using var scope = _serviceProvider.CreateScope();
        var before = scope.ServiceProvider.GetService<ITestEntityApiBefore>();
        for (var i = 0; i < QueryCount; i++)
        {
            var list = await before.GetList();
            _consumer.Consume(list);
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
    }
}