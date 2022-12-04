using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Interfaces;

public interface ISiteSettingsProvider
{

	/// <summary>
	/// Количество страниц в пагинации 
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<int> PaginationViewCountAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Количество элементов в списках ресурсов на одной странице
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<int> ResourcesPaginatedItemsCountAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Количество элементов в списках новостей на одной странице
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<int> NewsPaginatedItemsCountAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Количество элементов в результатах поиска на одной странице
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<int> GetSearchPaginatedItemsCountAsync(CancellationToken cancellationToken);
}
