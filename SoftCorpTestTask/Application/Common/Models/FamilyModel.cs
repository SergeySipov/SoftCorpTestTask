using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models;

public record FamilyModel : IMapFrom<Family>
{
    public int Id { get; init; }
    public string Title { get; init; }
}
