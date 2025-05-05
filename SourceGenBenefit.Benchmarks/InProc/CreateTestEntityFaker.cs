using Bogus;
using Bogus.Extensions;
using BeforeCreateTestEntity = SourceGenBenefit.Before.Create.CreateTestEntity;
using AfterCreateTestEntity = SourceGenBenefit.After.Create.CreateTestEntity;

namespace SourceGenBenefit.Benchmarks.InProc;

public sealed class CreateTestEntityFaker
{
    public BeforeCreateTestEntityFaker Before { get; } = new();

    public AfterCreateTestEntityFaker After { get; } = new();
}

public sealed class BeforeCreateTestEntityFaker : Faker<BeforeCreateTestEntity>
{
    public BeforeCreateTestEntityFaker()
    {
        RuleFor(x => x.Number, faker => faker.Random.Int(1, 128));
        RuleFor(x => x.Amount, faker => faker.Random.Decimal(-84m, 93m));
        RuleFor(
            x => x.Description,
            faker => faker.Lorem.Sentence(wordCount: 20, range: 5).ClampLength(max: 256));
    }
}

public sealed class AfterCreateTestEntityFaker : Faker<AfterCreateTestEntity>
{
    public AfterCreateTestEntityFaker()
    {
        RuleFor(x => x.Number, faker => faker.Random.Int(1, 128));
        RuleFor(x => x.Amount, faker => faker.Random.Decimal(-84m, 93m));
        RuleFor(
            x => x.Description,
            faker => faker.Lorem.Sentence(wordCount: 20, range: 5).ClampLength(max: 256));
    }
}