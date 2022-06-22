using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.IR.Interfaces.Dto
{
    public class EventImageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SortOrder { get; set; }
        public string ImageURL { get; set; }
    }
}
