using QA.DotNetCore.Engine.QpData;

namespace Demosite.Models.Pages
{
    public class NewsPage : AbstractPage
    {
        public int? CategoryId => GetDetail<int?>("CategoryId", null);

        public string DetailsText => GetDetail("DetailsText", string.Empty);
    }
}
