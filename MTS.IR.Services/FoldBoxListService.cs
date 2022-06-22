using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Postgre.DAL;
using System.Collections.Generic;
using System.Linq;

namespace MTS.IR.Services
{
    public class FoldBoxListService : IFoldBoxListService
    {
        public IDbContext QpDataContext { get; }
        public FoldBoxListService(IDbContext context)
        {
            this.QpDataContext = context;
        }
        public IEnumerable<FoldBoxListItemDto> GetAllItems()
        {
            return (QpDataContext as PostgreQpDataContext).FoldBoxListItems.Select(Map).ToArray();
        }

        public FoldBoxListItemDto GetItem(int id)
        {
            return Map((QpDataContext as PostgreQpDataContext).FoldBoxListItems.FirstOrDefault(f => f.Id == id));
        }

        public IEnumerable<FoldBoxListItemDto> GetItems(IEnumerable<int> itemIds)
        {
            return (QpDataContext as PostgreQpDataContext).FoldBoxListItems.Where(f => itemIds.Contains(f.Id)).Select(Map).ToArray();
        }

        private FoldBoxListItemDto Map(FoldBoxListItem item)
        {
            if (item == null)
                return null;
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
