using System.ComponentModel.DataAnnotations;
using Demosite.ViewModels.Interface;

namespace Demosite.ViewModels;

public class SearchBarViewModel : ISearchModel
{
	[Display(Prompt = "Поиск")]
	public string Query { get; }

	public SearchBarViewModel(string? query)
	{
		Query = query ?? string.Empty;
	}
}
