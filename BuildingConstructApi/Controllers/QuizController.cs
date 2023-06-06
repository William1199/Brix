using Application.System.Quizzes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Quizzes;

namespace BuildingConstructApi.Controllers
{
    [Route("api/quiz")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizServices _quizService;

        public QuizController(IQuizServices quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll([FromRoute] int id)
        {
            var userID = User.FindFirst("UserID").Value;


            var result = await _quizService.GetAll(id,Guid.Parse(userID));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest request)
        {
            var result = await _quizService.CreateQuiz(request);
            return Ok(result);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] QuizSubmit request)
        {
            var userID = User.FindFirst("UserID").Value;

            if (userID == null)
            {
                return BadRequest();
            }

            var result = await _quizService.QuizSubmit(request,Guid.Parse(userID));
            return Ok(result);
        }
    }
}
