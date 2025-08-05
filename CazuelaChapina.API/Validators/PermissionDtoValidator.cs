using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class PermissionDtoValidator : AbstractValidator<PermissionDto>
{
    public PermissionDtoValidator()
    {
        RuleFor(x => x.ModuleId).GreaterThan(0);
    }
}
