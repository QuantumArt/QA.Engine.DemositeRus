using MTS.IR.Interfaces.Dto;
using System.Collections.Generic;

namespace MTS.IR.Interfaces
{
    public interface IBannerWidgetService
    {
        IEnumerable<BannerItemDto> GetBanners(IEnumerable<int> ids);
    }
}
