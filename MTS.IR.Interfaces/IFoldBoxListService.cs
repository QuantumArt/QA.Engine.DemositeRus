using MTS.IR.Interfaces.Dto;
using System.Collections.Generic;

namespace MTS.IR.Interfaces
{
    public interface IFoldBoxListService
    {
        public IEnumerable<FoldBoxListItemDto> GetAllItems();
        public IEnumerable<FoldBoxListItemDto> GetItems(IEnumerable<int> itemIds);
        public FoldBoxListItemDto GetItem(int id);
    }
}
