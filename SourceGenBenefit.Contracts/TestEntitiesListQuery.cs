namespace SourceGenBenefit.Contracts;

public record TestEntitiesListQuery :
    MediatR.IRequest<IReadOnlyList<TestEntityDto>>,
    Mediator.IQuery<IReadOnlyList<TestEntityDto>>;