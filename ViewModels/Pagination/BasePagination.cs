using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Response;

namespace ViewModels.Pagination
{
    public class BasePagination<T> : BaseResponse<T>
    {
        public Pagination Pagination { get; set; }

        public BasePagination()
        {

        }


    }


    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

    }

}
