
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Infrastructure.Entitites;

public class AuthEntity
{
    [Key]
    [ForeignKey(nameof(CustomerEntity))]
    public Guid CustomerId { get; set; } 


    [Required]
    public string LoginName { get; set; } = null!;

    [Required]
    public string Pass { get; set; } = null!;

    public virtual CustomerEntity Customer { get; set; } = null!;

}
