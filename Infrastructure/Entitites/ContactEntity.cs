

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entitites;

public class ContactEntity
{
    [Key]

    public Guid CustomerId { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; }= null!;

    public string? PhoneNumber { get; set; }

    public  virtual CustomerEntity Customer { get; set; }= null!;    

}
