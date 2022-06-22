using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels.Builders
{
    public class BannerWidgetViewModelBuilder
    {
        private IBannerWidgetService _service { get; }
        public BannerWidgetViewModelBuilder(IBannerWidgetService service)
        {
            this._service = service;
        }

        public BannerListViewModel Build(IEnumerable<int> ids)
        {
            var result = new BannerListViewModel();
            var items = _service.GetBanners(ids).Select(Map).ToList();
            result.Items = items;
            return result;
        }

        private BannerItemViewModel Map (BannerItemDto model)
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
