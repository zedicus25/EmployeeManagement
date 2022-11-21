using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class UsersRole
{
    public int Id { get; set; }
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeesRole> EmployeesRoles { get; } = new List<EmployeesRole>();
}
