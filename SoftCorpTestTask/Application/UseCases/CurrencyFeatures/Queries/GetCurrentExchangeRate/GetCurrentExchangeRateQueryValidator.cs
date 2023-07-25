using FluentValidation;

namespace Application.UseCases.CurrencyFeatures.Queries.GetCurrentExchangeRate;

public class GetCurrentExchangeRateQueryValidator : AbstractValidator<GetCurrentExchangeRateQuery>
{
    public GetCurrentExchangeRateQueryValidator()
    {
        RuleFor(g => g.FromCurrencyCode)
            .NotEmpty()
            .MaximumLength(3);

        RuleFor(g => g.ToCurrencyCode)
            .NotEmpty()
            .MaximumLength(3);
    }
}
