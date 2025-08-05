using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class CreateInventoryEntryDtoValidator : AbstractValidator<CreateInventoryEntryDto>
{
    public CreateInventoryEntryDtoValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
