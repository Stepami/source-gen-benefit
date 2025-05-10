using Mediator;
using SourceGenBenefit.After;
using SourceGenBenefit.After.Api.Infrastructure;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddAfter(nullLogger: false, nullRepository: false);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
var todosApi = app.MapGroup("/after/test-entity");
todosApi.MapGet("/", (IMediator mediator) => mediator.Send(new TestEntitiesListQuery()));
todosApi.MapPost("/", (CreateTestEntity entity, IMediator mediator) => mediator.Send(new CreateTestEntityCommand(entity)));
todosApi.MapDelete("/", (ITestEntityRepository repository) => repository.Clear());

app.Run();