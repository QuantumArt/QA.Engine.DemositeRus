using QA.DotNetCore.Engine.QpData;

namespace MTS.IR.Models.Pages
{
    public class NewsPage : AbstractPage
    {
        public int? CategoryId => GetDetail<int?>("CategoryId", null);

        public string DetailsText => GetDetail("DetailsText", string.Empty);
    }
}
