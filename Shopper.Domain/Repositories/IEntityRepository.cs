using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopper.Domain
{
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
