using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.ApplicationServices;
using Services.Interfaces;
using WebApi.FIlter;
using WebApi.Helper;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiResultFilter]
    public class RequestCourseController : Controller
    {
        private readonly IRequestCourseRepository _requestCourseRepository;
        public RequestCourseController(IRequestCourseRepository requestCourseRepository)
        {
            _requestCourseRepository = requestCourseRepository;
        }

        //[Authorize(Permission.Permisson.AccessToFinishedCourses)]
        //[HttpGet("GetRequestCourse")]
        //public IActionResult GetRequestCourse(int reqId)
        //{
        //    string curUser = User.FindFirst(ClaimTypes.Name)?.Value;
        //    var reqCourses = _requestCourseRepository.Get(Int32.Parse(curUser), reqId).Result;
        //    if (reqCourses is null)
        //        return NoContent();
        //    return Ok(reqCourses);
        //}

        [Authorize(Permission.Permisson.AccessToFinishedCourses)]
        [HttpGet("GetFinishedRequestCourses")]
        public IActionResult GetFinishedRequestCourses()
        {
            string curUser = User.FindFirst(ClaimTypes.Name)?.Value;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            var reqCourses = _requestCourseRepository.GetAllFinishedRequestCourses(Int32.Parse(role), Int32.Parse(curUser)).Result;
            if (reqCourses.Count() is 0)
                return NoContent();
            return Ok(reqCourses);
        }

        [Authorize(Permission.Permisson.AccessToAcceptRequestCourses)]
        [HttpGet("GetRequestCourseById")]
        public IActionResult GetRequestCourseById(int reqId)
        {
            string curUser = User.FindFirst(ClaimTypes.Name)?.Value;
            var reqCourses = _requestCourseRepository.Get(Int32.Parse(curUser), reqId).Result;
            if (reqCourses == null)
                return NoContent();
            return Ok(reqCourses);
        }

        [Authorize(Permission.Permisson.AccessToPerformingCourses)]
        [HttpGet("GetPerformingRequestCourses")]
        public IActionResult GetPerformingRequestCourses()
        {
            string curUser = User.FindFirst(ClaimTypes.Name)?.Value;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            var reqCourses = _requestCourseRepository.GetAllPerformingRequestCourses(Int32.Parse(role), Int32.Parse(curUser)).Result;
            if (reqCourses.Count() is 0)
                return NoContent();
            return Ok(reqCourses);
        }

        [Authorize(Permission.Permisson.AccessToNotConfirmedCourses)]
        [HttpGet("GetNotConfirmedRequestCourses")]
        public IActionResult GetNotConfirmedRequestCourses()
        {
            var reqCourses = _requestCourseRepository.GetAllNotConfirmedRequestCourses().Result;
            if (reqCourses.Count() == null)
                return NoContent();
            return Ok(reqCourses);
        }

        [Authorize(Permission.Permisson.AccessToRequestCourse)]
        [HttpPost("AddRequestCourse")]
        public IActionResult AddRequestCourse(ReqCourse reqCourse)
        {
            reqCourse.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _requestCourseRepository.Insert(reqCourse).Result;
            return Ok(res.ToString());
        }

        [Authorize(Permission.Permisson.AccessToAcceptRequestCourses)]
        [HttpPut("AccpetRequestCourse")]
        public IActionResult AccpetRequestCourse(AcceptCourse acceptCourse)
        {
            var forRes = new ForReturn();
            acceptCourse.editUser = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            int res = _requestCourseRepository.AcceptCourse(acceptCourse).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddAttachFile)]
        [HttpPost("AddAttachToRequestCourse")]
        public IActionResult AddAttachToRequestCourse([FromForm] string fileName, [FromForm] IFormFile file, [FromForm] int reqCourseId)
        {
            var forRes = new ForReturn();
            string regUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _requestCourseRepository.AddAttach(fileName, file, reqCourseId, Int32.Parse(regUser)).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToGetAttachedFiles)]
        [HttpGet("GetAttachedFile")]
        public IActionResult GetAttachedFile(int reqCourseId)
        {
            var res = _requestCourseRepository.GetAllAttach(reqCourseId).Result;
            if (res.Count() == 0)
                return NoContent();
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddAttachFile)]
        [HttpDelete("DeleteFile")]
        public IActionResult DeleteFile(int fileId)
        {
            string regUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _requestCourseRepository.DeleteAttach(fileId).Result;
            return Ok(res);
        }
    }
}
