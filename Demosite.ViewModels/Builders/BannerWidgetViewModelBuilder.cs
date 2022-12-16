using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class BannerWidgetViewModelBuilder
    {
        private readonly IBannerWidgetService _service;
        public BannerWidgetViewModelBuilder(IBannerWidgetService service)
        {
            _service = service;
        }

        public BannerListViewModel Build(IEnumerable<int> ids)
        {
            BannerListViewModel result = new();
            List<BannerItemViewModel> items = _service.GetBanners(ids).Select(Map).ToList();
            result.Items = items;
            return result;
        }

        private BannerItemViewModel Map(BannerItemDto model)
        {
            return new BannerItemViewModel()
            {
                Id = model.Id,
                Text = model.Text,
                SortOrder = model.SortOrder,
                Url = model.Url,
                ImageUrl = model.ImageUrl
            };
        }
    }
}
