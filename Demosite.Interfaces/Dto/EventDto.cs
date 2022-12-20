using System.Collections.Generic;

namespace Demosite.Interfaces.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string TextBelow { get; set; }
        public IEnumerable<EventImageDto> EventImages { get; set; }
    }
}
