using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Queries.Common;

public class UsersCostsSearchQueryBaseRequestValidator<T> : AbstractValidator<UsersCostsSearchQueryBaseRequest<T>>
{
    public UsersCostsSearchQueryBaseRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);

        RuleForEach(x => x.CostCategoriesIds)
            .GreaterThan(0);

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .When(x => x.UserId.HasValue);
    }
}
