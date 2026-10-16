using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Equipment
{
    public long EquipmentId { get; set; }

    public string AssetCode { get; set; }

    public string EquipmentName { get; set; }

    public string Description { get; set; }

    public long EquipmentCategoryId { get; set; }

    public long? LocationId { get; set; }

    public long CurrentConditionId { get; set; }

    public long OperationalStatusId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual ICollection<ConditionInspection> ConditionInspections { get; set; } = new List<ConditionInspection>();

    public virtual Condition CurrentCondition { get; set; }

    public virtual EquipmentCategory EquipmentCategory { get; set; }

    public virtual EquipmentInventoryBalance EquipmentInventoryBalance { get; set; }

    public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();

    public virtual Location Location { get; set; }

    public virtual Condition OperationalStatus { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
