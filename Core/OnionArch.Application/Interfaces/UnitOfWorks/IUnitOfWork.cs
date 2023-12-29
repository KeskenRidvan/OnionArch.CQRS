using OnionArch.Application.Interfaces.Repositories;

namespace OnionArch.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
	IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, IEntityBase, new();
	IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : class, IEntityBase, new();
	Task<int> SaveAsync();
	int Save();
}
