using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Interfaces;

public interface ISiteSettingsService
{

    /// <summary>
    /// Количество страниц в пагинации 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> PaginationViewCountAsync(CancellationToken cancellationToken);

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


    /// <summary>
    /// Если при поиске не найдено ни одного результата (или найдено мало), поисковая система может предложить исправление запроса
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int?> GetSearchFoundLteAsync(CancellationToken cancellationToken);
}
