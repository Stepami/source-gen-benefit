using Bogus;
using Bogus.Extensions;

namespace SourceGenBenefit.Benchmarks.Api;

public record CreateTestEntityCommand(CreateTestEntity CreateTestEntity);

public record CreateTestEntity
{
    public int Number { get; set; }

    public decimal Amount { get; set; }

    public required string Description { get; set; }
}

public sealed class CreateTestEntityFaker : Faker<CreateTestEntity>
{
    public CreateTestEntityFaker()
    {
        RuleFor(x => x.Number, faker => faker.Random.Int(1, 128));
        RuleFor(x => x.Amount, faker => faker.Random.Decimal(-84m, 93m));
        RuleFor(
            x => x.Description,
            faker => faker.Lorem.Sentence(wordCount: 20, range: 5).ClampLength(max: 256));
    }
}