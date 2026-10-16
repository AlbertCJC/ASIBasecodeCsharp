using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Location
{
    public long LocationId { get; set; }

    public string LocationCode { get; set; }

    public string LocationName { get; set; }

    public bool IsPickupLocation { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
