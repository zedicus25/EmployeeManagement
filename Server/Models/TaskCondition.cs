using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class TaskCondition
{
    public int Id { get; set; }

    public int? DescriptionId { get; set; }

    public virtual Description? Description { get; set; }

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
