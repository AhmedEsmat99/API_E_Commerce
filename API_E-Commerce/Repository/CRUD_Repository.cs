using API_E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API_E_Commerce.Repository
{
    public class CRUD_Repository<T> : ICRUD_Repository<T> where T : class
    {
        Context db;
        public CRUD_Repository(Context context)
        {
            this.db = context;
        }
        public List<T> GetAll()
        {
            List<T> Entity = db.Set<T>().ToList();
            if (Entity == null)
                throw new ArgumentException(nameof(Entity), "Your Data is incorrect");
            else
            return db.Set<T>().ToList();
        }
        public T GetById(int id)
        {
             return  db.Set<T>().Find(id);
        }
        public T GetById(string id)
        {
            return db.Set<T>().Find(id);
        }
        public T GetByName(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).FirstOrDefault();
        }
        public T GetByEmail(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).FirstOrDefault();
        }
        public int Insert(T entity)
        {
            db.Set<T>().Add(entity);
            return db.SaveChanges();
        }
        public int Update(T entity , int id)
        {
            T ent = db.Set<T>().Find(id);
            db.Entry(ent).CurrentValues.SetValues(entity);
            db.Entry(ent).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int DeleteById(int id)
        {
            db.Set<T>().Remove(GetById(id));
            return db.SaveChanges();
        }
    }
}
