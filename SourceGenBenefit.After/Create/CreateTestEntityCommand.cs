using System.ComponentModel.DataAnnotations;
using Mediator;
using Microsoft.Extensions.Logging;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After.Create;

public record CreateTestEntityCommand(CreateTestEntity CreateTestEntity) : ICommand;

public partial class CreateTestEntityCommandHandler(
    ITestEntityRepository repository, 
    ILogger<CreateTestEntityCommandHandler> logger) : ICommandHandler<CreateTestEntityCommand>
{
    public async ValueTask<Unit> Handle(CreateTestEntityCommand request, CancellationToken ct)
    {
        var createData = request.CreateTestEntity;
        LogStarting(createData);
        using var validationResult = await createData.ValidateAsync();
        if (!validationResult.IsSuccess)
            throw new ValidationException(validationResult.GetProblemDetailsJson());
        var entity = createData.ToTestEntity();
        await repository.CreateTestEntity(entity, ct);
        LogCreated(entity.Id);
        return Unit.Value;
    }

    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Starting CreateTestEntityCommand {@CommandData}")]
    private partial void LogStarting(CreateTestEntity commandData);

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "Created TestEntity with Id {Id}")]
    private partial void LogCreated(Guid id);
}