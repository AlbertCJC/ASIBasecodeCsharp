using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Role
{
    public long RoleId { get; set; }

    public string RoleCode { get; set; }

    public string RoleName { get; set; }

    public bool IsBorrowerRole { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
