

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entitites;

public class ContactEntity
{
    [Key]
    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; }= null!;

    public string? PhoneNumber { get; set; }

    public  virtual CustomerEntity Customer { get; set; }= null!;    

}
