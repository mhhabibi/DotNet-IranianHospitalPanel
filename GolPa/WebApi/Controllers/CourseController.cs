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
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadCourses)]
        [HttpGet("GetCourses")]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetAll().Result;
            if (courses.Count() == 0)
                return NoContent();
            return Ok(courses);
        }

        [Authorize(Permission.Permisson.AccessToReadCourses)]
        [HttpGet("GetCourseById")]
        public IActionResult GetCourseById(int id)
        {
            var courses = _courseRepository.Get(id).Result;
            if (courses == null)
                return NoContent();
            return Ok(courses);
        }

        [Authorize(Permission.Permisson.AccessToReadCourses)]
        [HttpGet("GetCourseByGroupId")]
        public IActionResult GetCourseByGroupId(int groupId)
        {
            var courses = _courseRepository.GetCoursesByGroupId(groupId).Result;
            if (courses.Count() == 0)
                return NoContent();
            return Ok(courses);
        }

        [Authorize(Permission.Permisson.AccessToAddCourse)]
        [HttpPost("AddCourse")]
        public IActionResult AddCourse(Course course)
        {
            course.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _courseRepository.Insert(course).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddCourse)]
        [HttpPut("UpdateCourse")]
        public IActionResult UpdateCourse(Course course)
        {
            course.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _courseRepository.Update(course).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddCourse)]
        [HttpDelete("DeleteCourse")]
        public IActionResult DeleteCourse(long id)
        {
            int res = _courseRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
