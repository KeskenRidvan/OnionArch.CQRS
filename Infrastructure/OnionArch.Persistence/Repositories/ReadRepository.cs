using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnionArch.Application.Interfaces.Repositories;
using System.Linq.Expressions;

namespace OnionArch.Persistence.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IEntityBase, new()
{

	private readonly DbContext _dbContext;

	public ReadRepository(DbContext dbContext)
	{
		_dbContext = dbContext;
	}

	private DbSet<TEntity> Table { get => _dbContext.Set<TEntity>(); }

	public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool enableTracking = false)
	{
		IQueryable<TEntity> queryable = Table;

		if (enableTracking is not true)
			queryable = queryable.AsNoTracking();

		if (include is not null)
			queryable = include(queryable);

		if (predicate is not null)
			queryable.Where(predicate);

		if (orderBy is not null)
			return await orderBy(queryable).ToListAsync();

		return await queryable.ToListAsync();

	}

	public async Task<IList<TEntity>> GetAllByPagingAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
	{
		IQueryable<TEntity> queryable = Table;

		if (enableTracking is not true)
			queryable = queryable.AsNoTracking();

		if (include is not null)
			queryable = include(queryable);

		if (predicate is not null)
			queryable.Where(predicate);

		if (orderBy is not null)
			return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

		return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
	}

	public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false)
	{
		IQueryable<TEntity> queryable = Table;

		if (enableTracking is not true)
			queryable = queryable.AsNoTracking();

		if (include is not null)
			queryable = include(queryable);

		return await queryable.FirstOrDefaultAsync(predicate);
	}

	public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
	{
		Table.AsNoTracking();

		if (predicate is not null)
			Table.Where(predicate);

		return await Table.CountAsync();
	}

	public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false)
	{
		if (enableTracking is not true)
			Table.AsNoTracking();

		return Table.Where(predicate);
	}
}
