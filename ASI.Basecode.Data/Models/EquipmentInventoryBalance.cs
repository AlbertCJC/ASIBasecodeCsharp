using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class EquipmentInventoryBalance
{
    public long EquipmentId { get; set; }

    public int TotalQuantity { get; set; }

    public int ReservedQuantity { get; set; }

    public int ReleasedQuantity { get; set; }

    public int AwaitingInspectionQuantity { get; set; }

    public int MaintenanceQuantity { get; set; }

    public int UnreconciledQuantity { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual Equipment Equipment { get; set; }
}
