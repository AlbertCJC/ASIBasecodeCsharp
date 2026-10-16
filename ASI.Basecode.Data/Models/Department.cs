using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class Department
{
    public long DepartmentId { get; set; }

    public string DepartmentCode { get; set; }

    public string DepartmentName { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
