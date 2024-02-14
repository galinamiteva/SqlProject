using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    protected Repo(DataContext context)
    {
        _context = context;
    }


    //Create

    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    //Create Async
    public virtual async Task<TEntity> CreateAsync (TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;

    }


    //GetOne
    public virtual TEntity GetOne(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;

    }

    //GetOne Async
    public virtual async Task <TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity =await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            return entity!;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;

    }
    //GetAll
    public virtual IEnumerable<TEntity> GetAll()
    {
        try
        {
            return _context.Set<TEntity>().ToList();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }

    //Update
    public   TEntity Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
            
            if(entityToUpdate != null)
            {
                
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();

                return entityToUpdate;
            }            
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }

    // Update Async
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)

    {
        try
        {
            var entityToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);

            if (entityToUpdate != null)
            {
               
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }

    public virtual async Task<TEntity> UpdateOneAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch { }
        return null!;
    }

    //Delete
    public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
            if( entity != null)
            {
                _context.Set<TEntity>().Remove(entity!);
                _context.SaveChanges();

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return false;
    }

    //Exists method

    public virtual bool Exists (Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return _context.Set<TEntity>().Any(expression);
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return false;
    }

}
