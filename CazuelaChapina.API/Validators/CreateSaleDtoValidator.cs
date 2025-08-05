using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class CreateSaleDtoValidator : AbstractValidator<CreateSaleDto>
{
    public CreateSaleDtoValidator()
    {
        RuleFor(x => x.BranchId).GreaterThan(0);
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemDtoValidator());
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow).When(x => x.Date.HasValue);
    }
}

public class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemDto>
{
    public CreateSaleItemDtoValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x)
            .Must(x => (x.TamaleId.HasValue ^ x.BeverageId.HasValue))
            .WithMessage("Either TamaleId or BeverageId must be provided, but not both.");
    }
}
