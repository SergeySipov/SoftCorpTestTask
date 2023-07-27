using FluentValidation;

namespace Application.UseCases.UserFeatures.Commands.RevokeAccessToken;

public class RevokeAccessTokenCommandValidator : AbstractValidator<RevokeAccessTokenCommand>
{
    public RevokeAccessTokenCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .MaximumLength(50);
    }
}