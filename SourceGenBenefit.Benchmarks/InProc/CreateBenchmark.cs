using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;
using SourceGenBenefit.After;
using SourceGenBenefit.Before;
using SourceGenBenefit.Domain;
using BeforeCreateTestEntity = SourceGenBenefit.Before.Create.CreateTestEntity;
using BeforeCreateTestEntityCommand = SourceGenBenefit.Before.Create.CreateTestEntityCommand;
using AfterCreateTestEntity = SourceGenBenefit.After.Create.CreateTestEntity;
using AfterCreateTestEntityCommand = SourceGenBenefit.After.Create.CreateTestEntityCommand;

namespace SourceGenBenefit.Benchmarks.InProc;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class CreateBenchmark
{
    private readonly CreateTestEntityFaker _faker = new();

    private CreateBenchmarkConfig<MediatR.IMediator, BeforeCreateTestEntity> _before;
    private CreateBenchmarkConfig<Mediator.IMediator, AfterCreateTestEntity> _after;

    [Params(1, 10, 100)] public int TestEntitiesCount;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var beforeServiceProvider = new ServiceCollection().AddBefore().BuildServiceProvider();
        _before = new CreateBenchmarkConfig<MediatR.IMediator, BeforeCreateTestEntity>(
            beforeServiceProvider,
            beforeServiceProvider.GetRequiredService<MediatR.IMediator>(),
            _faker.Before.Generate(TestEntitiesCount).ToList(),
            beforeServiceProvider.GetRequiredService<ITestEntityRepository>());
        
        var afterServiceProvider = new ServiceCollection().AddAfter().BuildServiceProvider();
        _after = new CreateBenchmarkConfig<Mediator.IMediator, AfterCreateTestEntity>(
            afterServiceProvider,
            afterServiceProvider.GetRequiredService<Mediator.IMediator>(),
            _faker.After.Generate(TestEntitiesCount).ToList(),
            afterServiceProvider.GetRequiredService<ITestEntityRepository>());

    }

    [Benchmark]
    public async Task BeforeSourceGen()
    {
        for (var i = 0; i < _before.CreateTestEntities.Count; i++)
        {
            var command = new BeforeCreateTestEntityCommand(_before.CreateTestEntities[i]);
            await _before.Mediator.Send(command);
        }
    }
    
    [Benchmark]
    public async Task AfterSourceGen()
    {
        for (var i = 0; i < _after.CreateTestEntities.Count; i++)
        {
            var command = new AfterCreateTestEntityCommand(_after.CreateTestEntities[i]);
            await _after.Mediator.Send(command);
        }
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _before.Dispose();
        _after.Dispose();
    }

    private record CreateBenchmarkConfig<TMediator, TCreateTestEntity>(
        ServiceProvider ServiceProvider,
        TMediator Mediator,
        IReadOnlyList<TCreateTestEntity> CreateTestEntities,
        ITestEntityRepository Repository) : IDisposable
    {
        public void Dispose()
        {
            Repository.Clear();
            ServiceProvider.Dispose();
        }
    }
}