using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Fio
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public virtual Person? Person { get; set; }
}
