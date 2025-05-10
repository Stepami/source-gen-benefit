namespace SourceGenBenefit.Contracts;

public record TestEntityDto
{
    public int Number { get; set; }

    public decimal Amount { get; set; }

    public required string Description { get; set; }

    public bool Flag { get; set; }
}