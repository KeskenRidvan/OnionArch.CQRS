using AutoMapper;
using AutoMapper.Internal;

namespace OnionArch.Mapper.AutoMappers;

public class Mapper : Application.Interfaces.AutoMapper.IMapper
{

	public static List<TypePair> typePairs = new();
	private IMapper MapperContainer;

	public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
	{
		Config<TDestination, TSource>(5, ignore);
		return MapperContainer.Map<TSource, TDestination>(source);
	}

	public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources, string? ignore = null)
	{
		Config<TDestination, TSource>(5, ignore);
		return MapperContainer.Map<IList<TSource>, IList<TDestination>>(sources);
	}

	public TDestination Map<TDestination>(object source, string? ignore = null)
	{
		Config<TDestination, object>(5, ignore);
		return MapperContainer.Map<TDestination>(source);
	}

	public IList<TDestination> Map<TDestination>(IList<object> sources, string? ignore = null)
	{
		Config<TDestination, IList<object>>(5, ignore);
		return MapperContainer.Map<IList<TDestination>>(sources);
	}

	protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
	{
		var typePair = new TypePair(typeof(TSource), typeof(TDestination));

		if (typePairs.Any(tp => tp.DestinationType.Equals(typePair.DestinationType) && tp.SourceType.Equals(typePair.SourceType) && ignore is null))
			return;

		typePairs.Add(typePair);

		var config = new MapperConfiguration(configuration =>
		{
			foreach (var item in typePairs)
			{
				if (ignore is not null)
					configuration.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ForMember(ignore, x => x.Ignore()).ReverseMap();
				else
					configuration.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
			}
		});

		MapperContainer = config.CreateMapper();
	}
}
