using FluentValidation;

namespace Application.UseCases.UserFeatures.Commands.AuthorizeUserAndCreateTokens;

public class AuthenticateUserAndGetJwtTokenCommandValidator : AbstractValidator<AuthenticateUserAndGetJwtTokenCommand>
{
    public AuthenticateUserAndGetJwtTokenCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(u => u.Password)
            .NotEmpty();
    }
}