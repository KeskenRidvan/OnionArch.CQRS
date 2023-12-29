using Microsoft.EntityFrameworkCore.Query;
using OnionArch.Domain.Common;
using System.Linq.Expressions;

namespace OnionArch.Application.Interfaces.Repositories;

public interface IReadRepository<TEntity> where TEntity : class, IEntityBase, new()
{
	Task<IList<TEntity>> GetAllAsync(
		Expression<Func<TEntity, bool>>? predicate = null,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		bool enableTracking = false);

	Task<IList<TEntity>> GetAllByPagingAsync(
		Expression<Func<TEntity, bool>>? predicate = null,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		bool enableTracking = false,
		int currentPage = 1, int pageSize = 3);


	Task<TEntity> GetAsync(
		Expression<Func<TEntity, bool>>? predicate,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
		bool enableTracking = false);

	Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

	IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false);
}
