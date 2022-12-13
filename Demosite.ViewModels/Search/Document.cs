namespace Demosite.ViewModels.Search;

public class Document
{
    public string Title { get; }
    public string SearchUrl { get; }
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
