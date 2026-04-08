using System;
using System.Collections.Generic;

namespace PractWarehouse2.ModelsDB;

public partial class User
{
    public int UserId { get; set; }

    public string UserLastname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? UserPatronymic { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserRoleId { get; set; }

    public virtual UserRole UserRole { get; set; } = null!;
}
