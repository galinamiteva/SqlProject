

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entitites;

public class CustomerEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;
    
    [Required]  

    public int  AddressId { get; set; }

    public virtual AddressEntity Address { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;


    //public virtual AuthEntity Auth { get; set; } = null!;

    //public virtual ContactEntity Contact { get; set; } = null!;
}
