using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Commands.Delete;

public class DeleteUsersCostCommandValidator : AbstractValidator<DeleteUsersCostCommand>
{
    public DeleteUsersCostCommandValidator()
    {
        RuleFor(d => d.UsersCostId)
            .GreaterThan(0);

        RuleFor(d => d.CurrentUserId)
            .GreaterThan(0);
    }
}
