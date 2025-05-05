using Dapper;
using Npgsql;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before.Api.Infrastructure;

public class TestEntityRepository(NpgsqlDataSource dataSource) : ITestEntityRepository
{
    public async Task CreateTestEntity(TestEntity entity, CancellationToken ct = default)
    {
        await using var cnn = await dataSource.OpenConnectionAsync(ct);
        await cnn.ExecuteAsync(
            "insert into test_entities values (@id, @number, @amount, @description, @flag, @createdAt)",
            entity);
        await cnn.CloseAsync();
    }

    public async Task<IReadOnlyList<TestEntity>> GetTestEntities(CancellationToken ct = default)
    {
        await using var cnn = await dataSource.OpenConnectionAsync(ct);
        var enumerable = await cnn.QueryAsync<TestEntity>(
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
        await cnn.CloseAsync();
        return enumerable.ToList();
    }

    public void Clear()
    {
        using var cnn = dataSource.OpenConnection();
        cnn.Execute("truncate table test_entities");
        cnn.Close();
    }
}