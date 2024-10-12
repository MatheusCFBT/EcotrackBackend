using System.Linq.Expressions;
using Ecotrack.Context;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace EcotrackData.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly EcotrackDbContext ecotrackDb;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(EcotrackDbContext ecotrackDbContext)
        {
            ecotrackDb = ecotrackDbContext;
            DbSet = ecotrackDbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);;
        }

        public Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            return SaveChanges(); 
        }

        public async Task<int> SaveChanges()
        {
            return await ecotrackDb.SaveChangesAsync();
        }

        public void Dispose()
        {
            ecotrackDb?.Dispose();
        }
    }
}