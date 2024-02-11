using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class AddressRepository(DataContext context) : Repo<AddressEntity>(context)
{
    private readonly DataContext _context=context;


    //        //GetAll
    //public override IEnumerable<AddressEntity> GetAll()
    //{
    //    try
    //    {
    //        var existingEntities = _context.Addresses
    //            .Include(i=>i.Customers)
    //            .ToList();
    //        if (existingEntities != null)
    //        {
    //            return existingEntities;
    //        }


    //    }
    //    catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
    //    return null!;
    //}


    //public override AddressEntity GetOne(Expression<Func<AddressEntity, bool>> expression)
    //{
    //    try
    //    {
    //        var existingEntity = _context.Addresses
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
