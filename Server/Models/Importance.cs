using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Importance
{
    public int Id { get; set; }

    public int DescriptionId { get; set; }

    public virtual Description Description { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
