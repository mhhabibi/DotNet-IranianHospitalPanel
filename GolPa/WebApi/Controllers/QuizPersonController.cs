using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizPersonController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        public QuizPersonController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet("GetQuizPersonByQuizId")]
        public IActionResult GetQuizPersonByQuizId(long id)
        {
            var forRes = new ForReturn();
            try
            {
                var user = _questionRepository.GetAllQuizPerson(id).Result;
                forRes.ResCode = 1;
                forRes.Message = "با موفقیت اجرا شد";
                forRes.Info = user;
            }
            catch (Exception)
            {
                forRes.ResCode = -1;
                forRes.Message = "مشکلی وجود دارد !";
            }
            return Ok(forRes);

        }

        [HttpGet("GetQuizPersonById")]
        public IActionResult GetQuizPersonById(int id)
        {
            var forRes = new ForReturn();
            try
            {
                var user = _questionRepository.GetQuizPersonById(id).Result;
                forRes.ResCode = 1;
                forRes.Message = "با موفقیت اجرا شد";
                forRes.Info = user;
                return Ok(forRes);
            }
            catch (Exception)
            {
                forRes.ResCode = -1;
                forRes.Message = "چنین آیتمی وجود ندارد !";
                return BadRequest(forRes);
            }

        }

        [HttpPost("AddQuizPerson")]
        [Authorize(Roles ="Professor,Admin")]
        public IActionResult AddQuizPerson(QuizPerson quizPerson)
        {
            var forRes = new ForReturn();
            try
            {
                int res = _questionRepository.InsertQuizPerson(quizPerson).Result;
                string msg = null;
                if (res > 0)
                {
                    res = 1;
                    msg = "با موفقیت ثبت شد !";
                }
                else if (res == -1)
                    msg = "چنین امتحانی وجود ندارد !";
                else if (res <= 0)
                    msg = "چنین پرسنلی موجود نیست  !";

                forRes.ResCode = res;
                forRes.Message = msg;
                return Ok(forRes);
            }
            catch (Exception)
            {
                forRes.ResCode = -400;
                forRes.Message = "مشکلی در ارسال داده ها وجود دارد !";
                return BadRequest(forRes);
            }

            
        }

        [HttpPut("UpdateQuizPerson")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult UpdateQuizPerson(QuizPerson quizPerson)
        {
            var forRes = new ForReturn();
            try
            {
                int res = _questionRepository.UpdateQuizPerson(quizPerson).Result;
                string msg = null;
                if (res > 0)
                {
                    res = 1;
                    msg = "با موفقیت ویرایش شد !";
                }
                else if (res == -1)
                    msg = "چنین امتحانی وجود ندارد !";
                else if (res <= 0)
                    msg = "چنین پرسنلی موجود نیست  !";

                forRes.ResCode = res;
                forRes.Message = msg;
                return Ok(forRes);
            }
            catch (Exception)
            {
                forRes.ResCode = -400;
                forRes.Message = "مشکلی در ارسال داده ها وجود دارد !";
                return BadRequest(forRes);
            }
        }

    }
}
