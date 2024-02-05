using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ContactRepository(DataContext context) : Repo<ContactEntity>(context)
    {
        private readonly DataContext _context = context;

        public override IEnumerable<ContactEntity> GetAll()
        {
            try
            {
                var existingEntities = _context.Contacts
                    .Include(i => i.Customer)
                    .ToList();
                if (existingEntities != null)
                {
                    return existingEntities;
                }


            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return null!;
        }

        public override ContactEntity GetOne(Expression<Func<ContactEntity, bool>> expression)
        {
            try
            {
                var existingEntity = _context.Contacts
                    .Include(i => i.Customer)
                    .FirstOrDefault(expression);
                if (existingEntity != null)
                {
                    return existingEntity;
                }


            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return null!;
        }
    }

}
