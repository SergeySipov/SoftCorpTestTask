using FluentValidation;

namespace Application.UseCases.FamilyFeatures.Commands.Create;

public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
{
    public CreateFamilyCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}