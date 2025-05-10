using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;
using SourceGenBenefit.After;
using SourceGenBenefit.Before;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Benchmarks.InProc;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class CreateBenchmark
{
    private readonly CreateTestEntityFaker _faker = new();

    private CreateBenchmarkConfig<MediatR.IMediator> _before;
    private CreateBenchmarkConfig<Mediator.IMediator> _after;

    [Params(1, 10, 100)] public int TestEntitiesCount;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var beforeServiceProvider = new ServiceCollection().AddBefore().BuildServiceProvider();
        _before = new CreateBenchmarkConfig<MediatR.IMediator>(
            beforeServiceProvider,
            beforeServiceProvider.GetRequiredService<MediatR.IMediator>(),
            _faker.Generate(TestEntitiesCount).ToList(),
            beforeServiceProvider.GetRequiredService<ITestEntityRepository>());
        
        var afterServiceProvider = new ServiceCollection().AddAfter().BuildServiceProvider();
        _after = new CreateBenchmarkConfig<Mediator.IMediator>(
            afterServiceProvider,
            afterServiceProvider.GetRequiredService<Mediator.IMediator>(),
            _faker.Generate(TestEntitiesCount).ToList(),
            afterServiceProvider.GetRequiredService<ITestEntityRepository>());

    }

    [Benchmark]
    public async Task BeforeSourceGen()
    {
        for (var i = 0; i < _before.CreateTestEntities.Count; i++)
        {
            var command = new CreateTestEntityCommand(_before.CreateTestEntities[i]);
            await _before.Mediator.Send(command);
        }
    }
    
    [Benchmark]
    public async Task AfterSourceGen()
    {
        for (var i = 0; i < _after.CreateTestEntities.Count; i++)
        {
            var command = new CreateTestEntityCommand(_after.CreateTestEntities[i]);
            await _after.Mediator.Send(command);
        }
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _before.Dispose();
        _after.Dispose();
    }

    private record CreateBenchmarkConfig<TMediator>(
        ServiceProvider ServiceProvider,
        TMediator Mediator,
        IReadOnlyList<CreateTestEntity> CreateTestEntities,
        ITestEntityRepository Repository) : IDisposable
    {
        public void Dispose()
        {
            Repository.Clear();
            ServiceProvider.Dispose();
        }
    }
}