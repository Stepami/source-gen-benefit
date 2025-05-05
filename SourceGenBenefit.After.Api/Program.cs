using Mediator;
using SourceGenBenefit.After;
using SourceGenBenefit.After.Api.Infrastructure;
using SourceGenBenefit.After.Create;
using SourceGenBenefit.After.GetList;
using SourceGenBenefit.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAfter(nullLogger: false, nullRepository: false);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
var todosApi = app.MapGroup("/after/test-entity");
todosApi.MapGet("/", (IMediator mediator) => mediator.Send(new TestEntitiesListQuery()));
todosApi.MapPost("/", (CreateTestEntityCommand command, IMediator mediator) => mediator.Send(command));
todosApi.MapDelete("/", (ITestEntityRepository repository) => repository.Clear());

app.Run();