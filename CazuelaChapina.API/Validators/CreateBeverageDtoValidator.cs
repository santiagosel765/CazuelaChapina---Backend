using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class CreateBeverageDtoValidator : AbstractValidator<CreateBeverageDto>
{
    public CreateBeverageDtoValidator()
    {
        RuleFor(x => x.TypeId).GreaterThan(0);
        RuleFor(x => x.SizeId).GreaterThan(0);
        RuleFor(x => x.SweetenerId).GreaterThan(0);
        RuleForEach(x => x.ToppingIds).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
