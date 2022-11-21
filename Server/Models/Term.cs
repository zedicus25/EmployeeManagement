using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Term
{
    public int Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ToComplete { get; set; }

    public virtual Task? Task { get; set; }
}
