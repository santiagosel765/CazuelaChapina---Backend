using CazuelaChapina.API.DTOs;
using FluentValidation;
using System.Linq;

namespace CazuelaChapina.API.Validators;

public class CreateComboDtoValidator : AbstractValidator<CreateComboDto>
{
    public CreateComboDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x)
            .Must(x => x.Tamales.Any() || x.Beverages.Any())
            .WithMessage("Combo must include at least one product");
        RuleForEach(x => x.Tamales).ChildRules(t =>
        {
            t.RuleFor(y => y.TamaleId).GreaterThan(0);
            t.RuleFor(y => y.Quantity).GreaterThan(0);
        });
        RuleForEach(x => x.Beverages).ChildRules(b =>
        {
            b.RuleFor(y => y.BeverageId).GreaterThan(0);
            b.RuleFor(y => y.Quantity).GreaterThan(0);
        });
    }
}
