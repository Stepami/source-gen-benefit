using Riok.Mapperly.Abstractions;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.After.GetList;

[Mapper]
public static partial class TestEntityDtoMapper
{
    public static partial IReadOnlyList<TestEntityDto> ToTestEntityDtoList(
        this IReadOnlyList<TestEntity> entities);

    [MapperIgnoreSource(nameof(TestEntity.Id))]
    [MapperIgnoreSource(nameof(TestEntity.CreatedAt))]
    private static partial TestEntityDto ToTestEntityDto(TestEntity entity);
}