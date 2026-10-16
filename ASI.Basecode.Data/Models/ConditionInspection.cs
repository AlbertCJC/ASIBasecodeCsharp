using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class ConditionInspection
{
    public long InspectionId { get; set; }

    public long? BorrowingReturnId { get; set; }

    public long BorrowingId { get; set; }

    public long EquipmentId { get; set; }

    public long? ReleaseConditionId { get; set; }

    public long CurrentConditionId { get; set; }

    public int InspectedQuantity { get; set; }

    public string InspectionNotes { get; set; }

    public long InspectedByUserId { get; set; }

    public long InspectionStatusId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public virtual Borrowing Borrowing { get; set; }

    public virtual BorrowingReturn BorrowingReturn { get; set; }

    public virtual Condition CurrentCondition { get; set; }

    public virtual Equipment Equipment { get; set; }

    public virtual User InspectedByUser { get; set; }

    public virtual InspectionStatus InspectionStatus { get; set; }

    public virtual Condition ReleaseCondition { get; set; }
}
