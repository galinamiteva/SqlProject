

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Dtos;

public class CustomerDto
{
    public Guid  Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;

    public string? PhoneNumber { get; set; }   
    public string Email { get; set; }=null!;
    public string StreetName { get; set; } = null!;
    public string City { get; set; }=null !;
    public string PostalCode { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string LoginName { get; set; } = null!;
    public string Pass { get; set; } = null!;




}
