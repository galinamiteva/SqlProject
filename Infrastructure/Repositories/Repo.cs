using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class Repo<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected Repo(DataContext context)
        {
            _context = context;
        }


        //Create
        public virtual TEntity Create (TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
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
        public virtual TEntity Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            try
            {
                var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
                
                if(entityToUpdate != null)
                {
                    //!!!!  PROVERI  entityToUpdate = entity;
                    _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                }


                return entityToUpdate!;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return null!;
        }

        public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = _context.Set<TEntity>().FirstOrDefault(expression);
                _context.Remove(entity!);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return false;
        }

        public bool Exists (Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return _context.Set<TEntity>().Any(expression);
            }
            catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
            return false;
        }

    }
}
