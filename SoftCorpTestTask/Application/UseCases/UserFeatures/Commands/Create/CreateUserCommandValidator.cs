using FluentValidation;

namespace Application.UseCases.UserFeatures.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);
        
        RuleFor(u => u.Password)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(u => u.Username)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(u => u.LastName)
            .NotEmpty()
            .MaximumLength(100);
    }
}