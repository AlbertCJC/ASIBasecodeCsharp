using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class User
{
    public long UserId { get; set; }

    public string AccountIdentifier { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string SchoolEmail { get; set; }

    public string PasswordHash { get; set; }

    public long RoleId { get; set; }

    public long? DepartmentId { get; set; }

    public long AccountStatusId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual AccountStatus AccountStatus { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Borrowing> BorrowingBorrowerUsers { get; set; } = new List<Borrowing>();

    public virtual ICollection<Borrowing> BorrowingReleasedByUsers { get; set; } = new List<Borrowing>();

    public virtual ICollection<BorrowingReturn> BorrowingReturns { get; set; } = new List<BorrowingReturn>();

    public virtual ICollection<ConditionInspection> ConditionInspections { get; set; } = new List<ConditionInspection>();

    public virtual Department Department { get; set; }

    public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Role Role { get; set; }
}
