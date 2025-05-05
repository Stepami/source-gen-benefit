namespace SourceGenBenefit.Before.Create;

public record CreateTestEntity
{
    public int Number { get; set; }

    public decimal Amount { get; set; }

    public required string Description { get; set; }
}