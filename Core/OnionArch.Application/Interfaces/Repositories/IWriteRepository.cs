using OnionArch.Domain.Common;

namespace OnionArch.Application.Interfaces.Repositories;

public interface IWriteRepository<TEntity> where TEntity : class, IEntityBase, new()
{
	Task AddAsync(TEntity entity);
	Task AddRangeAsync(IList<TEntity> entities);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task HardDeleteAsync(TEntity entity);
}
