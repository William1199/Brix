using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using ViewModels.Quizzes;
using ViewModels.Response;

namespace Application.System.Quizzes
{
    public class QuizServices : IQuizServices
    {

        private readonly BuildingConstructDbContext _context;

        public QuizServices(BuildingConstructDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<QuizDTO>> GetAll(int id, Guid userId)
        {
            BaseResponse<QuizDTO>? response = null;

            var quiz = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id == id);

            var user = await _context.Users.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();

            if (quiz != null && user != null)
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Data = MapToDTO(quiz, user),
                    Message = BaseCode.SUCCESS
                };
            }

            else
            {
                response = new()
                {
                    Code = BaseCode.ERROR_MESSAGE,
                    Message = BaseCode.NOTFOUND_MESSAGE
                };
            }

            return response;

        }

        public async Task<BaseResponse<string>> CreateQuiz(CreateQuizRequest request)
        {
            BaseResponse<string> response = new();

            Quiz quiz = new()
            {
                LastModifiedAt = DateTime.Now,
                Name = request.QuizName,
                PostID = request.PostId,
                TypeID = request.TypeId
            };

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();

            var post = await _context.ContractorPosts.FirstOrDefaultAsync(x => x.Id == request.PostId);

            if (post != null && post.Status != Status.SUCCESS)
            {
                var postTypes = await _context.ContractorPostTypes.Where(x => x.ContractorPostID == post.Id).CountAsync();
                var quizCount = await _context.Quizzes.Where(x => x.PostID == post.Id).CountAsync();
                if (postTypes == quizCount)
                {
                    post.Status = Status.SUCCESS;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
            }




            if (request.Questions != null)
            {

                foreach (var item in request.Questions)
                {
                    Question question = new()
                    {
                        Name = item.QuestionName,
                        QuizId = quiz.Id,
                    };
                    await _context.Questions.AddAsync(question);

                    var rs = await _context.SaveChangesAsync();
                    if (rs > 0)
                    {
                        if (item.Answers != null)
                        {
                            foreach (var answer in item.Answers)
                            {
                                Answer tmp = new()
                                {
                                    isCorrect = answer.IsCorrect,
                                    Name = answer.AnswerName,
                                    QuestionId = question.Id,
                                };
                                await _context.Answers.AddAsync(tmp);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE
            };

            return response;
        }

        public QuizDTO MapToDTO(Quiz quiz, User user)
        {
            List<QuestionDTO> questions = new();

            var questionDB = _context.Questions.Where(x => x.QuizId == quiz.Id).ToList();

            var check = _context.UserAnswers.Where(x => x.BuilderId == user.BuilderId && x.Answer.Question.QuizId == quiz.Id).FirstOrDefault();

            foreach (var item in questionDB)
            {
                QuestionDTO tmp = new()
                {
                    QuestionId = item.Id,
                    QuestionName = item.Name,
                    Answers = MapListAnswerDTO(item.Id),

                };

                questions.Add(tmp);
            }
            //var totalScore = _context.UserAnswers.Include(x => x.Answer).Where(x => x.BuilderId == 1 && x.Answer.isCorrect == true && x.Answer.Question.QuizId==1).Count();

            QuizDTO result = new()
            {
                QuizName = quiz.Name,
                QuizId = quiz.Id,
                Questions = questions,
                TotalQuestion = questionDB.Count,
                HasAlreadyAttemped = check != null
            };
            return result;
        }

        public List<AnswerDTO> MapListAnswerDTO(int questionID)
        {
            var listAnswers = _context.Answers.Where(x => x.QuestionId == questionID).ToList();
            var result = new List<AnswerDTO>();

            foreach (var item in listAnswers)
            {
                var answer = new AnswerDTO()
                {
                    AnswerId = item.Id,
                    AnswerName = item.Name,
                    IsCorrect = item.isCorrect
                };
                result.Add(answer);
            }


            return result;
        }

        public async Task<BaseResponse<string>> QuizSubmit(QuizSubmit request, Guid UserId)
        {
            BaseResponse<string> response = new();
            List<UserAnswer> ls = new();

            var user = await _context.Users.Where(x => x.Id.Equals(UserId)).FirstOrDefaultAsync();

            foreach (var item in request.AnswerId)
            {
                UserAnswer answer = new()
                {
                    AnswerID = item,
                    BuilderId = user.BuilderId.Value
                };

                ls.Add(answer);
            }


            await _context.AddRangeAsync(ls);

            await _context.SaveChangesAsync();

            var postId = await _context.Answers
                .Include(x => x.Question)
                    .ThenInclude(x => x.Quiz)
                .Where(x => x.Id == request.AnswerId.First())
                .Select(x => x.Question.Quiz.PostID)
                .FirstOrDefaultAsync();

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE
            };

            var alreadyApplied = await _context.AppliedPosts.Where(x => x.BuilderID == user.BuilderId && x.PostID == postId).FirstOrDefaultAsync();

            var quizId = await _context.Answers
                        .Include(x => x.Question)
                                        .Where(x => x.Id == request.AnswerId.First())
                        .Select(x => x.Question.QuizId)
                        .FirstOrDefaultAsync();


            if (alreadyApplied == null)
            {
                AppliedPost applied = new()
                {
                    BuilderID = user.BuilderId.Value,
                    PostID = postId,
                    //WishSalary = request.WishSalary,
                    Status = Status.NOT_RESPONSE,
                    AppliedDate = DateTime.Now,
                    QuizId = quizId

                };

                await _context.AppliedPosts.AddAsync(applied);
                await _context.SaveChangesAsync();
            }


            return response;

        }
    }
}
