using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class EmployeesRole
{
    public int Id { get; set; }

    public int DescriptionId { get; set; }

    public int UserRoleId { get; set; }

    public virtual Description Description { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual UsersRole UserRole { get; set; } = null!;
}
