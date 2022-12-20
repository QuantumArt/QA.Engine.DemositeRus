using Provider.Search.DTO.Response;

namespace Provider.Search;

public interface ISearchProvider
{
    /// <summary>
    /// Выполняет запрос на поиск слов для автодополнения введённого текста.
    /// Например пользователь вводит "мос", в результатах будет "москва" и всё что начинается с "мос"
    /// </summary>
    /// <param name="request">Строка для которой искать дополнение</param>
    /// <param name="roles">Список ролей доступных пользователю для поиска только в доступных ему данных</param>
    /// <returns>Список слов для дополнения.</returns>
    Task<string[]> CompleteAsync(string request, string[] roles, CancellationToken token);

    /// <summary>
    /// Выполняет запрос на поиск конкретных элементов содержащих текст.
    /// </summary>
    /// <param name="request">Строка запроса (пользовательский ввод)</param>
    /// <param name="roles">Список ролей доступных пользователю для поиска только в доступных ему данных</param>
    /// <param name="limit">Сколько выбирать элементов (для пейджинга)</param>
    /// <param name="offset">Сколько элементов пропустить при выборке (для пейджинга)</param>
    /// <param name="ifFoundLte">Нижний порог  начиная с которого исходный запрос будет корректироваться</param>
    /// <param name="withCorrect">Применять ли исправление искомого запроса при поиске результатов, если нет то результаты будут для исходного запроса но в результатах будет предложение для исправления самого запроса</param>
    /// <returns></returns>
    Task<SearchResponse> SearchAsync(string request, string[] roles, int limit, int offset, int? ifFoundLte, bool withCorrect, CancellationToken token);
}
