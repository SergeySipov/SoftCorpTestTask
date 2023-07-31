using FluentValidation;

namespace Application.UseCases.FamilyFeatures.Queries.GetById;

public class GetFamilyByIdQueryValidator : AbstractValidator<GetFamilyByIdQuery>
{
    public GetFamilyByIdQueryValidator()
    {
        RuleFor(g => g.Id)
            .GreaterThan(0);
    }
}