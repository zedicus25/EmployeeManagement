using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class Fio
{
    public int Id { get; set; }
    [StringLength(70, MinimumLength = 5)]
    public string FirstName { get; set; } = null!;
    [StringLength(70, MinimumLength = 5)]
    public string LastName { get; set; } = null!;
    [StringLength(70, MinimumLength = 5)]
    public string Patronymic { get; set; } = null!;

    public virtual Person? Person { get; set; }
}
