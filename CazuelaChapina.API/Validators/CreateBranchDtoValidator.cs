using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class CreateBranchDtoValidator : AbstractValidator<CreateBranchDto>
{
    public CreateBranchDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Manager).NotEmpty();
    }
}
