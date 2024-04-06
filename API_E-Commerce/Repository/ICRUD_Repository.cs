using API_E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace API_E_Commerce.Repository
{
    public interface ICRUD_Repository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        T GetByName(Expression<Func<T, bool>> predicate);
        T GetByEmail(Expression<Func<T, bool>> predicate);
        int Insert(T entity);
        int Update(T entity, int id);
        int DeleteById(int id);
        T GetById(string id);
    }
}