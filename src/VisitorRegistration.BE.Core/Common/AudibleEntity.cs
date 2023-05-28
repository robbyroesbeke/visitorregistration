// (c) Visitor Registration

namespace VisitorRegistration.BE.Core.Common;

public abstract class AudibleEntity : EntityBase
{
    public DateTime CreatedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }
}