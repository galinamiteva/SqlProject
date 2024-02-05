

using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class DataContext : DbContext

{
    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public  virtual DbSet<ContactEntity> Contacts { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public  virtual DbSet<AuthEntity> Auths { get; set; }    
}
