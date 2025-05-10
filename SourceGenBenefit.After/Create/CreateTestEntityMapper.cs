using Riok.Mapperly.Abstractions;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After.Create;

[Mapper]
public static partial class CreateTestEntityMapper
{
    [MapValue(nameof(TestEntity.Id), Use = nameof(GetId))]
    [MapValue(nameof(TestEntity.Flag), Use = nameof(GetFlag))]
    [MapValue(nameof(TestEntity.CreatedAt), Use = nameof(GetCreatedAt))]
    public static partial TestEntity ToTestEntity(this CreateTestEntity request);

    private static Guid GetId() => Guid.CreateVersion7();
    private static bool GetFlag() => DateTime.UtcNow.Ticks % 2 == 0;
    private static DateTime GetCreatedAt() => DateTime.UtcNow;
}