using System.ComponentModel.DataAnnotations;
using Demosite.ViewModels.Interface;

namespace Demosite.ViewModels;

public class SearchBarViewModel : ISearchModel
{
	[Display(Prompt = "Поиск")]
	public string Query { get; }

    public bool WithCorrection { get; }

    public SearchBarViewModel() : this(string.Empty, false)
    {  }

    public SearchBarViewModel(string? query, bool? withCorrection)
	{
		Query = query ?? string.Empty;
        WithCorrection = withCorrection ?? false;
	}
}
