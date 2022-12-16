using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Services
{
    public class BannerWidgetService : IBannerWidgetService
    {
        private readonly PostgreQpDataContext _dataContext;
        public BannerWidgetService(IDbContext context)
        {
            _dataContext = context as PostgreQpDataContext;
        }

        public IEnumerable<BannerItemDto> GetBanners(IEnumerable<int> ids)
        {
            BannerItem[] result = _dataContext.BannerItems.Where(b => ids.Contains(b.Id))
                                           .OrderBy(b => b.SortOrder ?? 0)
                                           .ToArray();
            return result.Select(Map).ToArray();
        }

        private BannerItemDto Map(Postgre.DAL.BannerItem model)
        {
            return new BannerItemDto()
            {
                Id = model.Id,
                SortOrder = model.SortOrder ?? 0,
                Text = model.Text,
                Url = model.URL,
                ImageUrl = string.IsNullOrEmpty(model.Image) ? "" : model.ImageUrl
            };
        }
    }
}
