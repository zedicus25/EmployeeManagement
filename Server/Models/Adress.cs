using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public partial class Adress
{
    public int Id { get; set; }
    [StringLength(90, MinimumLength = 5)]
    public string Country { get; set; } = null!;
    [StringLength(90, MinimumLength = 5)]
    public string City { get; set; } = null!;
    [StringLength(90, MinimumLength = 7)]
    public string Street { get; set; } = null!;
    [StringLength(10, MinimumLength = 1)]
    public string HouseNumber { get; set; } = null!;
    public string? FullAdress { get; set; }

    public virtual Person? Person { get; set; }
}
