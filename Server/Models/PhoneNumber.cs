using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class PhoneNumber
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string PhoneNumber1 { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
