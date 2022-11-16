using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Image
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Path { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
