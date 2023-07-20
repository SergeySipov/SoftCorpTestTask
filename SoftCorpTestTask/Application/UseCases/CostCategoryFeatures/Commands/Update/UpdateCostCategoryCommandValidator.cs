using FluentValidation;

namespace Application.UseCases.CostCategoryFeatures.Commands.Update;

public class UpdateCostCategoryCommandValidator : AbstractValidator<UpdateCostCategoryCommand>
{
    public UpdateCostCategoryCommandValidator()
    {
        RuleFor(c => c.Description)
            .MaximumLength(200);

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.Color)
            .Matches("^#(?:[0-9a-fA-F]{3,4}){1,2}$");
        
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}
