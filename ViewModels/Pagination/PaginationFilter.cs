using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Filter;

namespace ViewModels.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? _sortBy { get; set; }
        public int _orderBy { get; set; }
        public FilterRequest? FilterRequest { get; set; }


        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 25;
            _orderBy = -1;
        }

        public PaginationFilter(int pageNumber, int pageSize, string sortBy, int orderBy)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
            _sortBy = string.IsNullOrEmpty(sortBy) ? string.Empty : sortBy;
            _orderBy = orderBy > 0 ? 1 : orderBy;
        }


        public PaginationFilter(int pageNumber, int pageSize, string sortBy, int orderBy,FilterRequest request)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
            _sortBy = string.IsNullOrEmpty(sortBy) ? string.Empty : sortBy;
            _orderBy = orderBy > 0 ? 1 : orderBy;
            FilterRequest = request;
        }


    }
}
