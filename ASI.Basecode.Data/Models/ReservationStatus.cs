using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class ReservationStatus
{
    public long StatusId { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
