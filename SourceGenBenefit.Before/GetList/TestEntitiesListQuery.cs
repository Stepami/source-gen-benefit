using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before.GetList;

public record TestEntitiesListQuery : IRequest<IReadOnlyList<TestEntityDto>>;

public class TestEntitiesListQueryHandler(
    IMapper mapper,
    ITestEntityRepository repository, 
    ILogger<TestEntitiesListQueryHandler> logger) : IRequestHandler<TestEntitiesListQuery, IReadOnlyList<TestEntityDto>>
{
    public async Task<IReadOnlyList<TestEntityDto>> Handle(TestEntitiesListQuery request, CancellationToken ct)
    {
        logger.LogInformation("Started TestEntitiesListQuery");
        var entities = await repository.GetTestEntities(ct);
        logger.LogInformation("Got {Count} items", entities.Count);
        return mapper.Map<IReadOnlyList<TestEntityDto>>(entities);
    }
}