

namespace Infrastructure.Dtos;

public class CustomerDto
{
    public Guid  CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;

    public string? PhoneNubmer { get; set; }   
    public string Email { get; set; }=null!;
    public string StreetName { get; set; } = null!;
    public string City { get; set; }=null !;
    public int PostalCode { get; set; }

    public string RoleName { get; set; } = null!;

    public string LoginName { get; set; } = null!;
    public string Pass { get; set; } = null!;




}
