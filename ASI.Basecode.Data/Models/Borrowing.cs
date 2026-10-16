using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Borrowing
{
    public long BorrowingId { get; set; }

    public string TransactionNumber { get; set; }

    public long? ReservationId { get; set; }

    public long BorrowerUserId { get; set; }

    public long EquipmentId { get; set; }

    public long BorrowingPolicyVersionId { get; set; }

    public int ReleasedQuantity { get; set; }

    public long ReleaseConditionId { get; set; }

    public DateTime ReleasedAtUtc { get; set; }

    public long ReleasedByUserId { get; set; }

    public DateTime DueAtUtc { get; set; }

    public long StatusId { get; set; }

    public long InspectionStatusId { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual User BorrowerUser { get; set; }

    public virtual BorrowingPolicyVersion BorrowingPolicyVersion { get; set; }

    public virtual ICollection<BorrowingReturn> BorrowingReturns { get; set; } = new List<BorrowingReturn>();

    public virtual ICollection<ConditionInspection> ConditionInspections { get; set; } = new List<ConditionInspection>();

    public virtual Equipment Equipment { get; set; }

    public virtual InspectionStatus InspectionStatus { get; set; }

    public virtual Condition ReleaseCondition { get; set; }

    public virtual User ReleasedByUser { get; set; }

    public virtual Reservation Reservation { get; set; }

    public virtual BorrowingStatus Status { get; set; }
}
