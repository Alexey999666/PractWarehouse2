using System;
using System.Collections.Generic;

namespace PractWarehouse2.ModelsDB;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
