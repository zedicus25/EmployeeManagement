using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class UsersRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeesRole> EmployeesRoles { get; } = new List<EmployeesRole>();
}
