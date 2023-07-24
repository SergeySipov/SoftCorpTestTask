using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsStatisticByPeriod;

public class GetFamilyCostsStatisticByPeriodQueryValidator : AbstractValidator<GetFamilyCostsStatisticByPeriodQuery>
{
    public GetFamilyCostsStatisticByPeriodQueryValidator()
    {
        RuleFor(g => g.EndPeriodDate)
            .LessThanOrEqualTo(DateTime.Now);

        RuleFor(g => g.StartPeriodDate)
            .LessThanOrEqualTo(g => g.EndPeriodDate);
    }
}
