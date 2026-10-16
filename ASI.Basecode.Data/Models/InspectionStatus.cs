using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class InspectionStatus
{
    public long StatusId { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual ICollection<ConditionInspection> ConditionInspections { get; set; } = new List<ConditionInspection>();
}
