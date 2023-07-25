using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Commands.Create;

public class CreateUsersCostCommandValidator : AbstractValidator<CreateUsersCostCommand>
{
    public CreateUsersCostCommandValidator()
    {
        RuleFor(c => c.Comment)
            .MaximumLength(300);

        RuleFor(c => c.CostId)
            .GreaterThan(0);

        RuleFor(c => c.UserId)
            .GreaterThan(0);

        RuleFor(c => c.Price)
            .GreaterThan(0);

        RuleFor(c => c.CurrencyCode)
            .IsInEnum();

        RuleFor(c => c.Date)
            .LessThan(DateTime.Now);
    }
}
