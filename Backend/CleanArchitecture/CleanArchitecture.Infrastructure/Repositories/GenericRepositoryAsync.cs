using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Contexts;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
       

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public virtual async Task<T> GetByIdAsync(object id)
        {
            var entityType = typeof(T);
            var keyProperty = _dbContext.Model.FindEntityType(entityType).FindPrimaryKey().Properties[0];

            if (id.GetType() != keyProperty.ClrType)
            {
                throw new ArgumentException($"The type of ID ({id.GetType()}) does not match the type of the entity's key property ({keyProperty.ClrType}).");
            }

            var keyName = keyProperty.Name;
            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, keyName);
            var constant = Expression.Constant(id);
            var equality = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await _dbContext.Set<T>().SingleOrDefaultAsync(lambda);
        }


        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

       
    }
}
