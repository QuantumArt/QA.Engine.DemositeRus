using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace Demosite.Models.Widgets
{
    public class BannerWidget : AbstractWidget
    {
        public IEnumerable<int> BannerItemIds => GetRelationIds("BannerItemIds");
        public int? SwipeDelay => GetDetail<int?>("SwipeDelay", null);
    }
}
