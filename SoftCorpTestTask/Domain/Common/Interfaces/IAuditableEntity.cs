namespace Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    int? CreatedBy { get; set; }
    DateTime? CreatedDateTime { get; set; }
    int? UpdatedBy { get; set; }
    DateTime? UpdatedDateTime { get; set; }
}