using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Postgre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTS.IR.Services
{
    public class BannerWidgetService : IBannerWidgetService
    {
        private PostgreQpDataContext _dataContext { get; }
        public BannerWidgetService(IDbContext context)
        {
            this._dataContext = context as PostgreQpDataContext;
        }

        public IEnumerable<BannerItemDto> GetBanners(IEnumerable<int> ids)
        {
            var result = _dataContext.BannerItems.Where(b => ids.Contains(b.Id))
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
