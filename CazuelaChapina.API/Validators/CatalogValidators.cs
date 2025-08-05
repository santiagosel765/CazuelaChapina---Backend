using CazuelaChapina.API.DTOs;
using FluentValidation;

namespace CazuelaChapina.API.Validators;

public class CreateTamaleTypeDtoValidator : AbstractValidator<CreateTamaleTypeDto>
{
    public CreateTamaleTypeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateTamaleTypeDtoValidator : AbstractValidator<UpdateTamaleTypeDto>
{
    public UpdateTamaleTypeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateFillingDtoValidator : AbstractValidator<CreateFillingDto>
{
    public CreateFillingDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateFillingDtoValidator : AbstractValidator<UpdateFillingDto>
{
    public UpdateFillingDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateWrapperDtoValidator : AbstractValidator<CreateWrapperDto>
{
    public CreateWrapperDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateWrapperDtoValidator : AbstractValidator<UpdateWrapperDto>
{
    public UpdateWrapperDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateSpiceLevelDtoValidator : AbstractValidator<CreateSpiceLevelDto>
{
    public CreateSpiceLevelDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateSpiceLevelDtoValidator : AbstractValidator<UpdateSpiceLevelDto>
{
    public UpdateSpiceLevelDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateBeverageTypeDtoValidator : AbstractValidator<CreateBeverageTypeDto>
{
    public CreateBeverageTypeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateBeverageTypeDtoValidator : AbstractValidator<UpdateBeverageTypeDto>
{
    public UpdateBeverageTypeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateBeverageSizeDtoValidator : AbstractValidator<CreateBeverageSizeDto>
{
    public CreateBeverageSizeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateBeverageSizeDtoValidator : AbstractValidator<UpdateBeverageSizeDto>
{
    public UpdateBeverageSizeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateSweetenerDtoValidator : AbstractValidator<CreateSweetenerDto>
{
    public CreateSweetenerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateSweetenerDtoValidator : AbstractValidator<UpdateSweetenerDto>
{
    public UpdateSweetenerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateToppingDtoValidator : AbstractValidator<CreateToppingDto>
{
    public CreateToppingDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateToppingDtoValidator : AbstractValidator<UpdateToppingDto>
{
    public UpdateToppingDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
