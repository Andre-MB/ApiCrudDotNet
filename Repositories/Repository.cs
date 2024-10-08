﻿using ApiUdemy.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiUdemy.Repositories;

public class Repository<T>: IRepository<T> where T : class
{
    protected readonly ApiDbContext _context;

    public Repository(ApiDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
        // Funciona sem o AsNoTracking, ele serve para desligar gerenciar os estados dos objetos, se não foi excluido
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        //_context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        // _context.Entry(entity).State = EntityState.Modified;
        //_context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        //_context.SaveChanges();
        return entity;
    }

}
