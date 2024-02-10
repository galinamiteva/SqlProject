

namespace Infrastructure.Dtos;

public class ContactDto

{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public Guid CustomerId { get; set; }
}
