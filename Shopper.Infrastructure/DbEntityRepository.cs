using Microsoft.EntityFrameworkCore;
using Shopper.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public abstract class DbEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DbContext context;

        protected DbSet<TEntity> entities => context.Set<TEntity>();

        public DbEntityRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return entities.AnyAsync(p => p.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            var list = await entities.ToListAsync();

            return list;
        }

        public virtual Task<TEntity> GetAsync(int id)
        {
             return entities.FindAsync(id).AsTask();
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await GetAsync(id);
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            // context.Entry(entity).State = EntityState.Modified;            
            entities.Update(entity);
            return context.SaveChangesAsync();
        }
    }
}
