using System.ComponentModel.DataAnnotations;

namespace Demosite.ViewModels;

public class SearchRequest : ISearchModel
{
	[Required]
	[MinLength(3)]
	public string Query { get; set; } = default!;

	public int Limit { get; set; } = 20;

	public int Offset { get; set; }
}
