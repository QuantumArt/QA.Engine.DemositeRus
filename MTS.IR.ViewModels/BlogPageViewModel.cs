using System;
using System.Collections.Generic;

namespace MTS.IR.ViewModels
{
    public class BlogPageViewModel
    {
        public BlogPageViewModel()
        {
            Items = new List<BlogItemInListViewModel>();
        }

        public string Header { get; set; }
        public List<BlogItemInListViewModel> Items { get; set; }
    }
    public class BlogItemInListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }

        public string Image { get; set; }
        public string YoutubeVideoCode { get; set; }

        public string Url { get; set; }

        public bool Published { get; set; }

        public int Comments { get; set; }
    }

}
