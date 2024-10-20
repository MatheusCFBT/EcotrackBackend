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

        // Método para buscar cliente por expressão generica
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        // Método para buscar cliente por Id
        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);;
        }

        // Adiciona o cliente no banco de dados
        public Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            return SaveChanges(); 
        }

        // Salva as alterações no banco de dados
        public async Task<int> SaveChanges()
        {
            return await ecotrackDb.SaveChangesAsync();
        }

        // Limpa a memoria
        public void Dispose()
        {
            ecotrackDb?.Dispose();
        }
    }
}