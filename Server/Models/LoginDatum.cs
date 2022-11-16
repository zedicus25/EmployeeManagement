using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class LoginDatum
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee? Employee { get; set; }
}
