using Validly;
using Validly.Extensions.Validators.Common;
using Validly.Extensions.Validators.Numbers;
using Validly.Extensions.Validators.Strings;

namespace SourceGenBenefit.Contracts;

[Validatable]
public partial record CreateTestEntity
{
    [Between(1, 128)]
    public int Number { get; set; }

    [Between(-84.5, 93.7)]
    public decimal Amount { get; set; }

    [NotEmpty]
    [MaxLength(256)]
    public required string Description { get; set; }
}