using Bogus;
using Shopper.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class FakeEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IDictionary<int, TEntity> entities;

        public FakeEntityRepository(Faker<TEntity> faker)
        {
            // entities = new Dictionary<int, TEntity>();

            var list = faker.Generate(100);

            entities = list.ToDictionary(p=>p.Id);
            
        }

        public virtual Task AddAsync(TEntity entity)
        {
            entity.Id = entities.Values.Max(p => p.Id) + 1;

            entities.Add(entity.Id, entity);

            return Task.CompletedTask;
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return Task.FromResult(entities.ContainsKey(id));
        }

        public virtual Task<IEnumerable<TEntity>> GetAsync()
        {
            return Task.FromResult(entities.Values.AsEnumerable());
        }

        public virtual Task<TEntity> GetAsync(int id)
        {
            if (entities.TryGetValue(id, out TEntity entity))
            {
                return Task.FromResult(entity);
            }
            else
            {
                return Task.FromResult<TEntity>(null);
            }
        }

        public virtual Task RemoveAsync(int id)
        {
            entities.Remove(id);

            return Task.CompletedTask;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await RemoveAsync(entity.Id);

            entities.Add(entity.Id, entity);
        }
    }
}
