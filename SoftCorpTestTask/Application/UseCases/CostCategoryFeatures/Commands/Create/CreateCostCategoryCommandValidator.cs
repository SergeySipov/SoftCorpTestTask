using FluentValidation;

namespace Application.UseCases.CostCategoryFeatures.Commands.Create;

public class CreateCostCategoryCommandValidator : AbstractValidator<CreateCostCategoryCommand>
{
    public CreateCostCategoryCommandValidator()
    {
        RuleFor(c => c.Description)
            .MaximumLength(200);

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.Color)
            .Matches("^#(?:[0-9a-fA-F]{3,4}){1,2}$");
    }
}
