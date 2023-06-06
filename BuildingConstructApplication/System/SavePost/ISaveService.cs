using Data.Entities;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.SavePost;

namespace Application.System.SavePost
{
    public interface ISaveService
    {
        public Task<BaseResponse<string>> SavePost(SavePostRequest request);
        public Task<BasePagination<List<SavePostDetailDTO>>> GetSavePostByUsID(PaginationFilter filter);
        public Task<bool> DeleteSave(DeleteSaveRequest request);

        public BaseResponse<string> DetectFace(Mat image);

        //public bool CompareFaces(Mat IDCardFace, Mat LiveFace);
    }
}
