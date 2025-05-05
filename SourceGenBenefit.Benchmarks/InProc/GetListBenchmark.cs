using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;
using SourceGenBenefit.After;
using SourceGenBenefit.After.Create;
using SourceGenBenefit.Before;
using SourceGenBenefit.Domain;
using BeforeTestEntitiesListQuery = SourceGenBenefit.Before.GetList.TestEntitiesListQuery;
using AfterTestEntitiesListQuery = SourceGenBenefit.After.GetList.TestEntitiesListQuery;

namespace SourceGenBenefit.Benchmarks.InProc;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class GetListBenchmark
{
    private readonly CreateTestEntityFaker _faker = new();
    private readonly Consumer _consumer = new();

    private GetListBenchmarkConfig<MediatR.IMediator> _before;
    private GetListBenchmarkConfig<Mediator.IMediator> _after;

    [Params(1, 10, 100)] public int QueryCount;
    public const int ListSize = 10;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        var beforeServiceProvider = new ServiceCollection().AddBefore().BuildServiceProvider();
        _before = new GetListBenchmarkConfig<MediatR.IMediator>(
            beforeServiceProvider,
            beforeServiceProvider.GetRequiredService<MediatR.IMediator>(),
            beforeServiceProvider.GetRequiredService<ITestEntityRepository>());
        foreach (var entity in _faker.Before.Generate(ListSize))
        {
            await _before.Mediator.Send(new Before.Create.CreateTestEntityCommand(entity));
        }

        var afterServiceProvider = new ServiceCollection().AddAfter().BuildServiceProvider();
        _after = new GetListBenchmarkConfig<Mediator.IMediator>(
            afterServiceProvider,
            afterServiceProvider.GetRequiredService<Mediator.IMediator>(),
            afterServiceProvider.GetRequiredService<ITestEntityRepository>());
        foreach (var entity in _faker.After.Generate(ListSize))
        {
            await _after.Repository.CreateTestEntity(entity.ToTestEntity());
        }
    }

    [Benchmark]
    public async Task BeforeSourceGen()
    {
        for (var i = 0; i < QueryCount; i++)
        {
            var result = await _before.Mediator.Send(new BeforeTestEntitiesListQuery());
            _consumer.Consume(result);
        }
    }

    [Benchmark]
    public async Task AfterSourceGen()
    {
        for (var i = 0; i < QueryCount; i++)
        {
            var result = await _after.Mediator.Send(new AfterTestEntitiesListQuery());
            _consumer.Consume(result);
        }
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _before.Dispose();
        _after.Dispose();
    }

    private record GetListBenchmarkConfig<TMediator>(
        ServiceProvider ServiceProvider,
        TMediator Mediator,
        ITestEntityRepository Repository) : IDisposable
    {
        public void Dispose()
        {
            Repository.Clear();
            ServiceProvider.Dispose();
        }
    }
}