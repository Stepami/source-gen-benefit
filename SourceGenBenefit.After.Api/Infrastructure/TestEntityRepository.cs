using MapDataReader;
using Npgsql;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After.Api.Infrastructure;

public class TestEntityRepository(NpgsqlDataSource dataSource) : ITestEntityRepository
{
    public async Task CreateTestEntity(TestEntity entity, CancellationToken ct = default)
    {
        await using var cmd = dataSource.CreateCommand(
            $"insert into test_entities values ($1, $2, $3, $4, $5, $6)");
        cmd.Parameters.Clear();
        cmd.Parameters.Add(new() { Value = entity.Id });
        cmd.Parameters.Add(new() { Value = entity.Number });
        cmd.Parameters.Add(new() { Value = entity.Amount });
        cmd.Parameters.Add(new() { Value = entity.Description });
        cmd.Parameters.Add(new() { Value = entity.Flag });
        cmd.Parameters.Add(new() { Value = entity.CreatedAt });
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async Task<IReadOnlyList<TestEntity>> GetTestEntities(CancellationToken ct = default)
    {
        await using var cmd = dataSource.CreateCommand(
            """
            SELECT
                x.id as "Id",
                x."number" as "Number",
                x.amount as "Amount",
                x.description as "Description",
                x.flag as "Flag",
                x.created_at as "CreatedAt"
            FROM public.test_entities x;
            """);
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        return reader.ToTestEntity();
    }

    public void Clear()
    {
        using var cmd = dataSource.CreateCommand("truncate table test_entities");
        cmd.ExecuteNonQuery();
    }
}