using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using WebApi.FIlter;
using WebApi.Helper;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("[controller]")]
    public class QuizController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet("GetQuizzes")]
        [Authorize(Permission.Permisson.AccessToReadQuizzes)]
        public IActionResult GetQuizzes()
        {
            int curUser = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var quizes = _quizRepository.GetAll(curUser).Result;
            if (quizes.Count() == 0) return NoContent();
            return Ok(quizes);
        }


        [HttpGet("GetQuizById")]
        [Authorize(Permission.Permisson.AccessToReadQuizzes)]
        public IActionResult GetQuizById(int id)
        {
            int curUser = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var user = _quizRepository.GetById(id, curUser).Result;
            if (user == null) return NoContent();
            return Ok(user);
        }


        [HttpDelete("DeleteQuiz")]
        [Authorize(Permission.Permisson.AccessToAddQuiz)]
        public IActionResult DeleteQuiz(long id)
        {
            int res = _quizRepository.Delete(id).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddQuiz)]
        [HttpPost("AddQuizz")]
        public IActionResult AddQuizz(Quiz quiz)
        {
            var forRes = new ForReturn();
            quiz.RegUser = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            int res = _quizRepository.Insert(quiz).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddQuiz)]
        [HttpPut("UpdateQuiz")]
        public IActionResult UpdateQuiz(Quiz quiz)
        {
            int res = _quizRepository.Update(quiz).Result;
            return Ok(res);
        }

    }
}
