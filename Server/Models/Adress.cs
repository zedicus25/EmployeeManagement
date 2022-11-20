namespace Server.Models;

public partial class Adress
{
    public int Id { get; set; }

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string HouseNumber { get; set; } = null!;

    public string? FullAdress { get; set; }

    public virtual Person? Person { get; set; }
}
