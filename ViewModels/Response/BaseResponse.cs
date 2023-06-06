using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Response
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public int? NavigateId { get; set; }

        public BaseResponse()
        {
        }
        public BaseResponse(T data)
        {
            Code = string.Empty;
            Message = string.Empty;
            Data = data;
            NavigateId = 0;
        }

    }
}
