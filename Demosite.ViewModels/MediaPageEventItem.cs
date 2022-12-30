using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class MediaPageEventItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string TextBelow { get; set; }
        public List<MediaPageEventImageItem> Images { get; set; } = new();
    }
}
