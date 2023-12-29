using OnionArch.Application.Interfaces.Repositories;
using OnionArch.Application.Interfaces.UnitOfWorks;
using OnionArch.Persistence.Context;
using OnionArch.Persistence.Repositories;

namespace OnionArch.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _appDbContext;

	public UnitOfWork(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async ValueTask DisposeAsync() => await _appDbContext.DisposeAsync();
	public int Save() => _appDbContext.SaveChanges();
	public async Task<int> SaveAsync() => await _appDbContext.SaveChangesAsync();
	IReadRepository<TEntity> IUnitOfWork.GetReadRepository<TEntity>() => new ReadRepository<TEntity>(_appDbContext);
	IWriteRepository<TEntity> IUnitOfWork.GetWriteRepository<TEntity>() => new WriteRepository<TEntity>(_appDbContext);
}
