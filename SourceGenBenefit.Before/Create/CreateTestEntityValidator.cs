using FluentValidation;

namespace SourceGenBenefit.Before.Create;

public class CreateTestEntityValidator : AbstractValidator<CreateTestEntity>
{
    public CreateTestEntityValidator()
    {
        RuleFor(x => x.Number).InclusiveBetween(1, 128);
        RuleFor(x => x.Amount).InclusiveBetween(-84.5m, 93.7m);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(256);
    }
}