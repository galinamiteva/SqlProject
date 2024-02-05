using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AuthRepository(DataContext context) : Repo<AuthEntity>(context)
    {
        private readonly DataContext _context = context;

        public override IEnumerable<AuthEntity> GetAll()
        {
            try
            {
                var existingEntities = _context.Auths
                    .Include(i => i.Customer)
                    .ToList();
                if (existingEntities != null)
                {
                    return existingEntities;
                }


            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public override AuthEntity GetOne(Expression<Func<AuthEntity, bool>> expression)
        {
            try
            {
                var existingEntity = _context.Auths
                    .Include(i => i.Customer)
                    .FirstOrDefault(expression);
                if (existingEntity != null)
                {
                    return existingEntity;
                }


            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
    }

}
