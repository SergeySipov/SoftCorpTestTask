using FluentValidation;

namespace Application.UseCases.FamilyFeatures.Commands.AddUserToFamily;

public class AddOrUpdateUserToFamilyCommandValidator : AbstractValidator<AddOrUpdateUserToFamilyCommand>
{
    public AddOrUpdateUserToFamilyCommandValidator()
    {
        RuleFor(a => a.FamilyId)
            .GreaterThan(0);

        RuleFor(a => a.UserId)
            .GreaterThan(0);
    }
}