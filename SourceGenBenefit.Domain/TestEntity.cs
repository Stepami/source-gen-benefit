using MapDataReader;

namespace SourceGenBenefit.Domain;

[GenerateDataReaderMapper]
public class TestEntity
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool Flag { get; set; }

    public DateTime CreatedAt { get; set; }
}