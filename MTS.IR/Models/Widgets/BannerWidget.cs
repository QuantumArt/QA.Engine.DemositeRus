using QA.DotNetCore.Engine.QpData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.IR.Models.Widgets
{
    public class BannerWidget : AbstractWidget
    {
        public IEnumerable<int> BannerItemIds => GetRelationIds("BannerItemIds");
        public int? SwipeDelay => GetDetail<int?>("SwipeDelay", null);
    }
}
