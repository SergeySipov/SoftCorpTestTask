using FluentValidation;

namespace Application.UseCases.FamilyFeatures.Commands.Delete;

public class DeleteFamilyCommandValidator : AbstractValidator<DeleteFamilyCommand>
{
    public DeleteFamilyCommandValidator()
    {
        RuleFor(d => d.FamilyId)
            .GreaterThan(0);
    }
}