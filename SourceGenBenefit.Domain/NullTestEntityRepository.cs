namespace SourceGenBenefit.Domain;

public class NullTestEntityRepository : ITestEntityRepository
{
    private const int Capacity = 1024;
    private readonly List<TestEntity> _list = new(capacity: Capacity);

    private Task<IReadOnlyList<TestEntity>>? _listTask;

    public Task CreateTestEntity(TestEntity entity, CancellationToken ct = default)
    {
        if (_list.Count <= Capacity)
            _list.Add(entity);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<TestEntity>> GetTestEntities(CancellationToken ct = default)
    {
        if (_listTask is not null)
            return _listTask;
        IReadOnlyList<TestEntity> entities = _list;
        _listTask = Task.FromResult(entities);
        return _listTask;
    }

    public void Clear() => _list.Clear();
}