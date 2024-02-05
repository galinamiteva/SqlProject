
using System.ComponentModel.DataAnnotations;


namespace Infrastructure.Entitites;

public class AuthEntity
{
    [Key]
    public Guid CustomerId { get; set; } 


    [Required]
    public string LoginName { get; set; } = null!;

    [Required]
    public string Pass { get; set; } = null!;

    public virtual CustomerEntity Customer { get; set; } = null!;

}
