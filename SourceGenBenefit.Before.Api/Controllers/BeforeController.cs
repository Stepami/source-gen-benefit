using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before.Api.Controllers;

[ApiController]
[Route("before/test-entity")]
public class BeforeController : ControllerBase
{
    [HttpPost]
    public Task Create(
        CreateTestEntity createTestEntity,
        [FromServices] IMediator mediator) =>
        mediator.Send(new CreateTestEntityCommand(createTestEntity));

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