using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Provider.Search;

public static class SearchServiceCollectionExtension
{
	public static IServiceCollection AddSearch(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddOptions<SearchSettings>()
			.Bind(configuration.GetSection("Search"))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddHttpClient<SearchApiClient>();
		services.AddScoped<ISearchProvider, SearchProvider>();

		return services;
	}
}
