using Domain.Common.Interfaces;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime? CreatedDateTime { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }
}
