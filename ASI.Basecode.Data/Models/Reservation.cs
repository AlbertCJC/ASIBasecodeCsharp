using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Reservation
{
    public long ReservationId { get; set; }

    public string ReservationNumber { get; set; }

    public long BorrowerUserId { get; set; }

    public long EquipmentId { get; set; }

    public long PickupLocationId { get; set; }

    public int RequestedQuantity { get; set; }

    public int ApprovedQuantity { get; set; }

    public int HeldQuantity { get; set; }

    public DateTime RequestedStartAtUtc { get; set; }

    public DateTime ExpectedReturnAtUtc { get; set; }

    public string Purpose { get; set; }

    public long BorrowingPolicyVersionId { get; set; }

    public long StatusId { get; set; }

    public DateTime SubmittedAtUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual User BorrowerUser { get; set; }

    public virtual BorrowingPolicyVersion BorrowingPolicyVersion { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Equipment Equipment { get; set; }

    public virtual Location PickupLocation { get; set; }

    public virtual ReservationStatus Status { get; set; }
}
