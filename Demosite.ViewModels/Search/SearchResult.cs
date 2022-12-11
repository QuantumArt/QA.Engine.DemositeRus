using Provider.Search.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels;

public class SearchResult
{
	public int DocumentsCount { get; }

	public int PagesCount { get; }

	public IEnumerable<Document> Documents { get; } = default!;

    public SearchCorrection SearchCorrection { get; }

    public string OriginalQuery { get; }

    public SearchResult(SearchResponse response, int itemsPerPage, string originalQuery)
	{
		DocumentsCount = response.TotalCount;
		PagesCount = (response.TotalCount + itemsPerPage - 1) / itemsPerPage;

		if (response.TotalCount == 0)
		{
			return;
		}

		if (response.Documents is null or { Length: 0 })
		{
			throw new ArgumentException("Elastic found some results but return empty result set.", nameof(response.Documents));
		}

		Documents = response.Documents
			.Select(x => new Document(
				x.Title ?? string.Empty,
				x.SearchUrl ?? throw new InvalidOperationException("Can't process without link."),
				x.Description ?? string.Empty,
				x.Category ?? string.Empty))
			.ToArray();

        if(response.QueryCorrection != null)
        {
            var correction = response.QueryCorrection;
            SearchCorrection = new SearchCorrection(correction.Text, correction.Snippet, correction.ResultsAreCorrected);
        }
        OriginalQuery = originalQuery;
    }
}
