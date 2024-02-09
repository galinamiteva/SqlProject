﻿

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entitites;

public class AddressEntity
{
    [Key]

    public int Id { get; set; }

    [Required]
    public string StreetName { get; set; } = null!;

    [Required]
    public string City { get; set; }= null!;

    [Required]
    public int  PostalCode { get; set; }

    public virtual ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();

}
