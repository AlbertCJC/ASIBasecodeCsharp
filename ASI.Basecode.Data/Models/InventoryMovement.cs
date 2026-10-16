using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class InventoryMovement
{
    public long MovementId { get; set; }

    public long EquipmentId { get; set; }

    public string MovementType { get; set; }

    public string FromState { get; set; }

    public string ToState { get; set; }

    public int Quantity { get; set; }

    public string ReferenceNumber { get; set; }

    public long? PerformedByUserId { get; set; }

    public DateTime OccurredAtUtc { get; set; }

    public virtual Equipment Equipment { get; set; }

    public virtual User PerformedByUser { get; set; }
}
