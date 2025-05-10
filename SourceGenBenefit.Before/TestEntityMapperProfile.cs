using AutoMapper;
using SourceGenBenefit.Contracts;
using SourceGenBenefit.Domain;

namespace SourceGenBenefit.Before;

public class TestEntityMapperProfile : Profile
{
    public TestEntityMapperProfile()
    {
        CreateMap<CreateTestEntity, TestEntity>()
            .ForMember(x => x.Id, opt => opt.MapFrom(_ => Guid.CreateVersion7()))
            .ForMember(x => x.Flag, opt => opt.MapFrom(_ => DateTime.UtcNow.Ticks % 2 == 0))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            ;
        CreateMap<TestEntity, TestEntityDto>();
    }
}