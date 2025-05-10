using Mediator;
using Microsoft.Extensions.Logging;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After.GetList;

public partial class TestEntitiesListQueryHandler(
    ITestEntityRepository repository,
    ILogger<TestEntitiesListQueryHandler> logger) : IQueryHandler<TestEntitiesListQuery, IReadOnlyList<TestEntityDto>>
{
    public async ValueTask<IReadOnlyList<TestEntityDto>> Handle(TestEntitiesListQuery request, CancellationToken ct)
    {
        LogStarted();
        var entities = await repository.GetTestEntities(ct);
        LogCount(entities.Count);
        return entities.ToTestEntityDtoList();
    }

    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Started TestEntitiesListQuery")]
    private partial void LogStarted();

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Got {Count} items")]
    private partial void LogCount(int count);
}