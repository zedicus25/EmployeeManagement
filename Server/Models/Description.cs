using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class Description
{
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 5)]
    public string Title { get; set; } = null!;
    [DataType(DataType.MultilineText)]
    [StringLength(2500, MinimumLength = 10)]
    public string Description1 { get; set; } = null!;

    public virtual EmployeesRole? EmployeesRole { get; set; }

    public virtual Importance? Importance { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Task? Task { get; set; }

    public virtual TaskCondition? TaskCondition { get; set; }
}
