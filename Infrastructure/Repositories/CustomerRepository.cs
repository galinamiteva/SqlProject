using Infrastructure.Context;
using Infrastructure.Entitites;

namespace Infrastructure.Repositories
{
    public class CustomerRepository(DataContext context) : Repo<CustomerEntity>(context)
    {
        private readonly DataContext _context = context;
    }

}
