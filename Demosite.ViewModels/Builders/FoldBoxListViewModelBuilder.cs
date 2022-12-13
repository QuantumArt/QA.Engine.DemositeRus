using Demosite.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class FoldBoxListViewModelBuilder
    {
        private readonly IFoldBoxListService _foldBoxListService;

        public FoldBoxListViewModelBuilder(IFoldBoxListService foldBoxListService)
        {
            _foldBoxListService = foldBoxListService;
        }

        public FoldBoxListViewModel Build(IEnumerable<int> ids, string type)
        {
            FoldBoxListViewModel result = new()
            {
                WidgetType = type.Length > 0 ? type : "Default"
            };

            List<Interfaces.Dto.FoldBoxListItemDto> items = _foldBoxListService.GetItems(ids).OrderBy(i => i.SortOrder).ToList();
            result.Items.AddRange(items.Select(Map).ToList());
            return result;
        }

        private FoldBoxListItemViewModel Map(Interfaces.Dto.FoldBoxListItemDto item)
        {
            return item == null
                ? null
                : new FoldBoxListItemViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Text = item.Text
                };
        }
    }
}
