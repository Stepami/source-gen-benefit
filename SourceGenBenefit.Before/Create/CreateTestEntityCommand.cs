using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before.Create;

public record CreateTestEntityCommand(
    CreateTestEntity CreateTestEntity) : IRequest<Unit>;

public class CreateTestEntityCommandHandler(
    IValidator<CreateTestEntity> validator,
    IMapper mapper,
    ITestEntityRepository repository, 
    ILogger<CreateTestEntityCommandHandler> logger) : IRequestHandler<CreateTestEntityCommand, Unit>
{
    public async Task<Unit> Handle(CreateTestEntityCommand request, CancellationToken ct)
    {
        logger.LogInformation("Starting CreateTestEntityCommand {@CommandData}", request.CreateTestEntity);
        await validator.ValidateAndThrowAsync(request.CreateTestEntity, ct);
        var entity = mapper.Map<TestEntity>(request.CreateTestEntity);
        await repository.CreateTestEntity(entity, ct);
        logger.LogInformation("Created TestEntity with Id {Id}", entity.Id);
        return Unit.Value;
    }
}