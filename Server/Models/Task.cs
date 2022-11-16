using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Task
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int DescriptionId { get; set; }

    public int TaskConditionId { get; set; }

    public int ImportanceId { get; set; }

    public int TermId { get; set; }

    public virtual Description Description { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Importance Importance { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual TaskCondition TaskCondition { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;
}
