using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArch.Application.Interfaces.Repositories;
using OnionArch.Persistence.Context;
using OnionArch.Persistence.Repositories;

namespace OnionArch.Persistence;

public static class Registration
{
	public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

		services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
		services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

	}
}
