using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class Person
{
    public int Id { get; set; }

    public int FioId { get; set; }

    public int AdressId { get; set; }
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    public virtual Adress Adress { get; set; } = null!;

    public virtual ICollection<Email> Emails { get; } = new List<Email>();

    public virtual Employee? Employee { get; set; }

    public virtual Fio Fio { get; set; } = null!;

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; } = new List<PhoneNumber>();
}
