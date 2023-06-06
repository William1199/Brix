using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Carts;
using ViewModels.Pagination;
using ViewModels.Quizzes;
using ViewModels.Response;

namespace Application.System.Quizzes
{
    public interface IQuizServices
    {
        Task<BaseResponse<QuizDTO>> GetAll(int id,Guid userId);

        Task<BaseResponse<string>> CreateQuiz(CreateQuizRequest request);

        Task<BaseResponse<string>> QuizSubmit(QuizSubmit request , Guid UserId);

    }
}
