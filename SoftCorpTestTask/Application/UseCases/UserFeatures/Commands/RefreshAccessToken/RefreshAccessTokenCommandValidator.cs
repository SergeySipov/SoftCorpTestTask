using FluentValidation;

namespace Application.UseCases.UserFeatures.Commands.RefreshAccessToken;

public class RefreshAccessTokenCommandValidator : AbstractValidator<RefreshAccessTokenCommand>
{
    public RefreshAccessTokenCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.AccessToken)
            .NotEmpty()
            .MaximumLength(200);
    }
}