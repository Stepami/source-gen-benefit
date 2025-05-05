namespace SourceGenBenefit.Domain;

public interface ITestEntityRepository
{
    Task CreateTestEntity(TestEntity entity, CancellationToken ct = default);

    Task<IReadOnlyList<TestEntity>> GetTestEntities(CancellationToken ct = default);

    void Clear();
}