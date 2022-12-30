using System;

namespace Demosite.ViewModels.Search;

public class Document
{
    public string Title { get; }
    public string SearchUrl { get; }
    public UriKind SearchUrlKind { get; } = UriKind.Absolute;
    public string Description { get; }
    public string Category { get; }

    public Document(string title, string searchUrl, string description, string category)
    {
        Title = title;
        SearchUrl = searchUrl;
        Description = description;
        Category = category;
    }
}
