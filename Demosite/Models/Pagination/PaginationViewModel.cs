using Microsoft.AspNetCore.Http;

namespace Demosite.Models.Pagination;

public class PaginationViewModel
{
    public int MinValue { get; init; }
    public int MaxValue { get; init; }
    public int Current { get; init; }
    public QueryString BaseQuery { get; init; }
    public bool IsMoveNextAvailable => Current < MaxValue;
    public bool IsMovePreviousAvailable => Current > MinValue;
}
