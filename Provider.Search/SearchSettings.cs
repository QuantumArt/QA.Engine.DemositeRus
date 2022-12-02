using System.ComponentModel.DataAnnotations;

namespace Provider.Search;

public class SearchSettings
{
	[Required]
	public string BaseUrl { get; set; } = default!;

	[Required]
	public string SearchPath { get; set; } = default!;

	[Required]
	public string CompletionPath { get; set; } = default!;
}
