using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnionArch.Application;

public static class Registration
{
	public static void AddApplicaton(this IServiceCollection services)
	{
		var assembly = Assembly.GetExecutingAssembly();

		services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));
	}
}
