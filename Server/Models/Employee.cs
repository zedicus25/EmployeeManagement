using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int RoleId { get; set; }

    public int TaskId { get; set; }

    public int LoginDataId { get; set; }

    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }

    public string? Avatar { get; set; }

    public virtual LoginDatum LoginData { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;

    public virtual EmployeesRole Role { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
