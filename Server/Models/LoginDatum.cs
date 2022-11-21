using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class LoginDatum
{
    public int Id { get; set; }
    
    [StringLength(25, MinimumLength = 5)]
    public string Login { get; set; } = null!;
    [DataType(DataType.Password)]
    [StringLength(30, MinimumLength = 5)]
    public string Password { get; set; } = null!;

    public virtual Employee? Employee { get; set; }
}
