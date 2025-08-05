using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class AssignRoleRequestValidator : AbstractValidator<AssignRoleRequest>
{
    public AssignRoleRequestValidator()
    {
        RuleFor(x => x.RoleId).GreaterThan(0);
    }
}
