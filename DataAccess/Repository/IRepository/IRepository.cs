﻿using Model;
using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T>where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, object>>[]? inculdeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);


        T? GetOne(Expression<Func<T, object>>[]? inculdeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);

        T? Find(Expression<Func<T, bool>> expression, bool tracked = true);
        void Add(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void Commit();
    }
}
