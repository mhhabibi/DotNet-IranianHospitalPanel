using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebApi.FIlter;
using WebApi.Helper;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadQuestions)]
        [HttpGet("GetQuestions")]
        public IActionResult GetQuestions(long Id)
        {
            int personId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var questions = _questionRepository.GetAll(Id, personId).Result;
            if (questions.Count() == 0) return NoContent();
            return Ok(questions);
        }

        [Authorize(Permission.Permisson.AccessToReadQuestions)]
        [HttpGet("GetQuestionById")]
        public IActionResult GetQuestionById(int id)
        {
            int personId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var question = _questionRepository.Get(id, personId).Result;
            if (question == null) return NoContent();
            return Ok(question);

        }

        [Authorize(Permission.Permisson.AccessToAddQuestion)]
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion(QuestionModel questionModel)
        {
            int res = _questionRepository.Insert(questionModel).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddQuestion)]
        [HttpPut("UpdateQuestion")]
        public IActionResult UpdateQuestion(QuestionModel questionModel)
        {
            int res = _questionRepository.Update(questionModel).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddQuestion)]
        [HttpDelete("DeleteQuestion")]
        public IActionResult DeleteQuestion(long id)
        {
            int res = _questionRepository.Delete(id).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddQuestion)]
        [HttpPost("AddQuestionAnswer")]
        public IActionResult AddQuestionAnswer(QuestionAnswer questionAnswer)
        {
            var forRes = new ForReturn();
            try
            {
                questionAnswer.PersonId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
                var res = _questionRepository.InsertAnswer(questionAnswer).Result;
                string msg = (res == 0) ? "مشکلی وجود دارد !" : null;
                msg = (res > 0) ? "با موفقیت افزوده شد !" : msg;
                forRes.ResCode = res;
                forRes.Message = msg;
                return Ok(forRes);
            }
            catch (Exception)
            {
                forRes.Message = "چنین سوالی وجود ندارد !";
            }
            return Ok(forRes);
        }

        [Authorize(Permission.Permisson.AccessToAddQuestion)]
        [HttpPut("UpdateQuestionAnswer")]
        public IActionResult UpdateQuestionAnswer(QuestionAnswer questionAnswer)
        {
            var forRes = new ForReturn();
            try
            {
                questionAnswer.PersonId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
                var res = _questionRepository.UpdateAnswer(questionAnswer).Result;
                string msg = (res == 0) ? "مشکلی وجود دارد !" : null;
                msg = (res > 0) ? "با موفقیت افزوده شد !" : msg;
                forRes.ResCode = res;
                forRes.Message = msg;
                return Ok(forRes);
            }
            catch (Exception)
            {
                forRes.Message = "چنین سوالی وجود ندارد !";
            }
            return Ok(forRes);
        }

        [Authorize(Permission.Permisson.AccessToReadQuestions)]
        [HttpGet("GetQuestionAnswer")]
        public IActionResult GetQuestionAnswer(int qId, int personId)
        {
            var forRes = new ForReturn();
            try
            {
                var question = _questionRepository.GetAllQuestionAnswer(qId, personId).Result;
                forRes.ResCode = 1;
                forRes.Message = "با موفقیت اجرا شد";
                forRes.Info = question;
            }
            catch (Exception)
            {
                forRes.ResCode = -1;
                forRes.Message = "سوالی با چنین آیدی وجود ندارد !";
            }
            return Ok(forRes);
        }

        [Authorize(Permission.Permisson.AccessToReadQuestions)]
        [HttpGet("GetQuestionAnswerById")]
        public IActionResult GetQuestionAnswerById(int Id)
        {
            var forRes = new ForReturn();
            try
            {
                var question = _questionRepository.GetQuestionAnswerById(Id).Result;
                forRes.ResCode = 1;
                forRes.Message = "با موفقیت اجرا شد";
                forRes.Info = question;
            }
            catch (Exception)
            {
                forRes.ResCode = -1;
                forRes.Message = "سوالی با چنین آیدی وجود ندارد !";
            }
            return Ok(forRes);
        }

    }
}
