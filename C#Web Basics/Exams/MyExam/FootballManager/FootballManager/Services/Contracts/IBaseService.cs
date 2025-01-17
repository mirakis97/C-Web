﻿using System.Linq;

namespace FootballManager.Services.Contracts
{
    public interface IBaseService<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllAsNoTracking();
        void Add(TEntity entity);
        int SaveChanges();
    }
}
