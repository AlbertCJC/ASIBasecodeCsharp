using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class AuditLog
{
    public long AuditLogId { get; set; }

    public DateTime OccurredAtUtc { get; set; }

    public long? ActorUserId { get; set; }

    public string ActionType { get; set; }

    public string EntityType { get; set; }

    public long? EntityId { get; set; }

    public string ReferenceNumber { get; set; }

    public string Details { get; set; }

    public virtual User ActorUser { get; set; }
}
