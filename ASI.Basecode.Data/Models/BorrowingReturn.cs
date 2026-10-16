using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class BorrowingReturn
{
    public long ReturnId { get; set; }

    public long BorrowingId { get; set; }

    public int ReturnedQuantity { get; set; }

    public DateTime ReturnedAtUtc { get; set; }

    public long ReceivedByUserId { get; set; }

    public bool WasReturnedLate { get; set; }

    public int DaysOverdue { get; set; }

    public virtual Borrowing Borrowing { get; set; }

    public virtual ICollection<ConditionInspection> ConditionInspections { get; set; } = new List<ConditionInspection>();

    public virtual User ReceivedByUser { get; set; }
}
