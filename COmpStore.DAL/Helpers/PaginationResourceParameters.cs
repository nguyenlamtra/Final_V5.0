using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COmpStore.DAL.Helpers
{
    public abstract class PaginationResourceParameters
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 3;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        //public string Search { get; set; }
        //public bool HasSearch { get { return !String.IsNullOrEmpty(Search); } }
        public string OrderBy { get; set; } = "ProductName";
        public bool Descending
        {
            get
            {
                if (!String.IsNullOrEmpty(OrderBy))
                {
                    return OrderBy.Split(' ').Last().ToLower().StartsWith("desc");
                }
                return false;
            }
        }
    }
}
