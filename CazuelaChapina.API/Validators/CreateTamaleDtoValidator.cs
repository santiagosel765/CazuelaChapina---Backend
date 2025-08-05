using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class CreateTamaleDtoValidator : AbstractValidator<CreateTamaleDto>
{
    public CreateTamaleDtoValidator()
    {
        RuleFor(x => x.TamaleTypeId).GreaterThan(0);
        RuleFor(x => x.FillingId).GreaterThan(0);
        RuleFor(x => x.WrapperId).GreaterThan(0);
        RuleFor(x => x.SpiceLevelId).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
