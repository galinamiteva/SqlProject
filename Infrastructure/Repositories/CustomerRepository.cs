using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CustomerRepository(DataContext context) : Repo<CustomerEntity>(context)
    {
        private readonly DataContext _context = context;

        public override IEnumerable<CustomerEntity> GetAll()
        {
            try
            {
                return _context.Customers
                    .Include(x=>x.Role)
                    .Include(x=>x.Contact)
                    .Include(x=>x.Auth)
                    .Include(x=>x.Address)
                    .ToList();

            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return null!;
        }





        public override CustomerEntity GetOne(Expression<Func<CustomerEntity, bool>> expression)
        {
            try
            {
                var result = _context.Customers
                    .Include(x=>x.Role)
                    .Include(x=>x.Contact)
                    .Include(x => x.Auth)
                    .Include(x => x.Address)                    
                    .FirstOrDefault(expression);
                return result!;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return null!;
        }

        public override async Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> expression)
        {
            try
            {
                var result = await _context.Customers
                    .Include(x => x.Role)
                    .Include(x => x.Contact)
                    .Include(x => x.Auth)
                    .Include(x => x.Address)                    
                    .FirstOrDefaultAsync(expression);
                return result!;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return null!;
        }
    }

}
