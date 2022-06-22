using Demosite.Interfaces.Dto;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IBannerWidgetService
    {
        IEnumerable<BannerItemDto> GetBanners(IEnumerable<int> ids);
    }
}
