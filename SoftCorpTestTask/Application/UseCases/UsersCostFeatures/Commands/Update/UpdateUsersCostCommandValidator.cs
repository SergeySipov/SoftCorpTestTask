using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Commands.Update;

public class UpdateUsersCostCommandValidator : AbstractValidator<UpdateUsersCostCommand>
{
    public UpdateUsersCostCommandValidator()
    {
        RuleFor(c => c.Comment)
            .MaximumLength(300);

        RuleFor(c => c.CostId)
            .GreaterThan(0);

        RuleFor(c => c.UserId)
            .GreaterThan(0);

        RuleFor(c => c.Price)
            .GreaterThan(0);

        RuleFor(c => c.Date)
            .LessThan(DateTime.Now);

        RuleFor(d => d.CurrentUserId)
            .GreaterThan(0);
    }
}
