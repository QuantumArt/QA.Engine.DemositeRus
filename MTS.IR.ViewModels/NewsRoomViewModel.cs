using System;
using System.Collections.Generic;

namespace MTS.IR.ViewModels
{
    public class NewsRoomViewModel
    {
        public string Title { get; set; }
        public List<NewsRoomBlockViewModel> Blocks { get; set; } = new List<NewsRoomBlockViewModel>();
    }

    public class NewsRoomBlockViewModel
    {
        public string Title { get; set; }
        public List<NewsRoomBlockItemViewModel> Items { get; set; } = new List<NewsRoomBlockItemViewModel>();
        public string Url { get; set; }
    }

    public class NewsRoomBlockItemViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime? PostData { get; set; }
        public string Brief { get; set; }
    }
}
