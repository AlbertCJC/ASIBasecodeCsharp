using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class AccountStatus
{
    public long AccountStatusId { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
