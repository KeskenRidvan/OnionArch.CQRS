using Microsoft.EntityFrameworkCore;
using OnionArch.Application.Interfaces.Repositories;

namespace OnionArch.Persistence.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class, IEntityBase, new()
{
	private readonly DbContext _dbContext;
	public WriteRepository(DbContext dbContext)
	{
		_dbContext = dbContext;
	}

	private DbSet<TEntity> Table { get => _dbContext.Set<TEntity>(); }

	public async Task AddAsync(TEntity entity) => await Table.AddAsync(entity);
	public async Task AddRangeAsync(IList<TEntity> entities) => await Table.AddRangeAsync(entities);
	public async Task<TEntity> UpdateAsync(TEntity entity) => await Task.Run(() => { Table.Update(entity); return entity; });
	public async Task HardDeleteAsync(TEntity entity) => await Task.Run(() => Table.Remove(entity));
}
