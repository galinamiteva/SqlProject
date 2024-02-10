

namespace Infrastructure.Dtos;

public class AuthDto
{
    public Guid CustomerId { get; set; }

    public string LoginName { get; set; } = null!;

    public string Pass { get; set; } = null!;
}
