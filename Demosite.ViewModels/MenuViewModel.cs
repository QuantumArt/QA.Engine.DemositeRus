using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class MenuViewModel
    {
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Href { get; set; }
        public List<MenuItem> Children { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool HasActiveChild { get; set; }

        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
    }
}
