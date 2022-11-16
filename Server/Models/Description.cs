using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Description
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description1 { get; set; } = null!;

    public virtual EmployeesRole? EmployeesRole { get; set; }

    public virtual Importance? Importance { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Task? Task { get; set; }

    public virtual TaskCondition? TaskCondition { get; set; }
}
