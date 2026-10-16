using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Condition
{
    public long ConditionId { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public bool AffectsAvailability { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual ICollection<ConditionInspection> ConditionInspectionCurrentConditions { get; set; } = new List<ConditionInspection>();

    public virtual ICollection<ConditionInspection> ConditionInspectionReleaseConditions { get; set; } = new List<ConditionInspection>();

    public virtual ICollection<Equipment> EquipmentCurrentConditions { get; set; } = new List<Equipment>();

    public virtual ICollection<Equipment> EquipmentOperationalStatuses { get; set; } = new List<Equipment>();
}
