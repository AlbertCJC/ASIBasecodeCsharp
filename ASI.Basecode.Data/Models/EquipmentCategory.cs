using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models;

public partial class EquipmentCategory
{
    public long EquipmentCategoryId { get; set; }

    public string CategoryCode { get; set; }

    public string CategoryName { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
