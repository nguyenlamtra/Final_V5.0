using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.Paging
{
    public class PageOutput<T> where T : class
    {
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public IList<T> Items { get; set; }
    }
}
