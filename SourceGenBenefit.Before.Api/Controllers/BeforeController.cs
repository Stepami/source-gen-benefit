using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceGenBenefit.Before.Create;
using SourceGenBenefit.Before.GetList;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before.Api.Controllers;

[ApiController]
[Route("before/test-entity")]
public class BeforeController : ControllerBase
{
    [HttpPost]
    public Task Create(
        CreateTestEntityCommand command,
        [FromServices] IMediator mediator) => mediator.Send(command);

    [HttpGet]
    public Task<IReadOnlyList<TestEntityDto>> GetList(
        [FromServices] IMediator mediator) =>
        mediator.Send(new TestEntitiesListQuery());

    [HttpDelete]
    public Task Delete([FromServices] ITestEntityRepository repository)
    {
        repository.Clear();
        return Task.CompletedTask;
    }
}