namespace SourceGenBenefit.Contracts;

public record CreateTestEntityCommand(CreateTestEntity CreateTestEntity) :
    MediatR.IRequest<MediatR.Unit>,
    Mediator.ICommand;