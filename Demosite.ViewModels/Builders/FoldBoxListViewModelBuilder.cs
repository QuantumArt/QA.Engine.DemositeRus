using Demosite.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class FoldBoxListViewModelBuilder
    {
        private IFoldBoxListService FoldBoxListService { get; }

        public FoldBoxListViewModelBuilder(IFoldBoxListService foldBoxListService)
        {
            this.FoldBoxListService = foldBoxListService;
        }

        public FoldBoxListViewModel Build(IEnumerable<int> ids, string type)
        {
            var result = new FoldBoxListViewModel
            {
                WidgetType = type.Length > 0 ? type : "Default"
            };

            var items = FoldBoxListService.GetItems(ids).OrderBy(i => i.SortOrder).ToList();
            result.Items.AddRange(items.Select(Map).ToList());
            return result;
        }

        private FoldBoxListItemViewModel Map(Interfaces.Dto.FoldBoxListItemDto item)
        {
            if (item == null)
                return null;
            return new FoldBoxListItemViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Text = item.Text
            };
        }
    }
}
