using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Services
{
    public class FoldBoxListService : IFoldBoxListService
    {
        private readonly IDbContext _qpDataContext;
        public FoldBoxListService(IDbContext context)
        {
            _qpDataContext = context;
        }
        public IEnumerable<FoldBoxListItemDto> GetAllItems()
        {
            return (_qpDataContext as PostgreQpDataContext).FoldBoxListItems.Select(Map).ToArray();
        }

        public FoldBoxListItemDto GetItem(int id)
        {
            return Map((_qpDataContext as PostgreQpDataContext).FoldBoxListItems.FirstOrDefault(f => f.Id == id));
        }

        public IEnumerable<FoldBoxListItemDto> GetItems(IEnumerable<int> itemIds)
        {
            return (_qpDataContext as PostgreQpDataContext).FoldBoxListItems.Where(f => itemIds.Contains(f.Id)).Select(Map).ToArray();
        }

        private FoldBoxListItemDto Map(FoldBoxListItem item)
        {
            if (item == null)
            {
                return null;
            }
            return new FoldBoxListItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Text = item.Text,
                SortOrder = item.SortOrder ?? 0
            };
        }
    }
}
