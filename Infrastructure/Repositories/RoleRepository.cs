using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class RoleRepository(DataContext context) : Repo<RoleEntity>(context)
{
    private readonly DataContext _context = context;

    //public override IEnumerable<RoleEntity> GetAll()
    //{
    //    try
    //    {
    //        var existingEntities = _context.Roles
    //            .Include(i => i.Customers)
    //            .ToList();
    //        if (existingEntities != null)
    //        {
    //            return existingEntities;
    //        }


    //    }
    //    catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
    //    return null!;
    //}

    //public override RoleEntity GetOne(Expression<Func<RoleEntity, bool>> expression)
    //{
    //    try
    //    {
    //        var existingEntity = _context.Roles
    //            .Include(i => i.Customers)
    //            .FirstOrDefault(expression);
    //        if (existingEntity != null)
    //        {
    //            return existingEntity;
    //        }


    //    }
    //    catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
    //    return null!;
    //}
}
