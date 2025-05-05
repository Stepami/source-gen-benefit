using WebApiClientCore.Attributes;

namespace SourceGenBenefit.Benchmarks.Api;

public interface ITestEntityApiBase
{
    [HttpGet]
    Task<IReadOnlyList<TestEntityDto>> GetList(CancellationToken ct = default);

    [HttpPost]
    Task Create([JsonContent] CreateTestEntityCommand command, CancellationToken ct = default);

    [HttpDelete]
    Task Clear(CancellationToken ct = default);
}

[HttpHost("http://localhost:5299/before/test-entity")]
public interface ITestEntityApiBefore : ITestEntityApiBase;

[HttpHost("http://localhost:5251/after/test-entity")]
public interface ITestEntityApiAfter : ITestEntityApiBase;