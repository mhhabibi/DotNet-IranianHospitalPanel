using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ViewModels;
using WebApi.FIlter;
using WebApi.Helper;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("[controller]")]
    public class PersonelController : Controller
    {
        private readonly IPersonelRepository _personelRepository;
        public PersonelController(IPersonelRepository personelRepository)
        {
            _personelRepository = personelRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadPersonels)]
        [HttpGet("GetPersonels")]
        public IActionResult GetPersonels()
        {
            IEnumerable<UserViewModel> users = _personelRepository.GetAll().Result;
            if (users.Count() == 0) return NoContent();
            return Ok(users);
        }

        [Authorize(Permission.Permisson.AccessToCurrentUser)]
        [HttpGet("GetCurrentUser")]
        public IActionResult GetCurrentUser()
        {
            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _personelRepository.Get(Int64.Parse(userId)).Result;
            return Ok(user);
        }


        [Authorize(Permission.Permisson.AccessToReadProfessors)]
        [HttpGet("GetProfessorsByCourseId")]
        public IActionResult GetProfessorsByCourseId(int courseId)
        {
            var professors = _personelRepository.GetProfessorByCourseId(courseId).Result;
            if (professors.Count() == 0) return NoContent();
            return Ok(professors);
        }

        [Authorize(Permission.Permisson.AccessToAddPersonel)]
        [HttpPost("AddPersonel")]
        public IActionResult AddPersonal([FromForm] UserPost userPost)
        {
            userPost.RoleId = (userPost.IsProfessor) ? 3 : 4;
            userPost.RoleId = (userPost.IsAdmin) ? 1 : userPost.RoleId;
            userPost.Permissions = (userPost.RoleId != 1) ? null : userPost.Permissions;
            userPost.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            string[] extentions = { "jpg", "jpeg" };
            if (userPost.Image != null)
            {
                string fileExt = userPost.Image.FileName.Split(".").Last().ToLower();
                userPost.Image = (Array.Exists(extentions, el => el == fileExt)) ? userPost.Image : null;
            }

            userPost.DoctorExpert = (userPost.IsDoctor == false) ? null : userPost.DoctorExpert;
            userPost.ProfessorCourses = (userPost.IsProfessor == false) ? null : userPost.ProfessorCourses;
            int res = _personelRepository.Insert(userPost).Result;

            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToReadPersonels)]
        [HttpGet("GetPersonelById")]
        public IActionResult GetPersonelById(int id)
        {
            var user = _personelRepository.Get(id).Result;
            if (user == null) return NoContent();
            return Ok(user);
        }

        [Authorize(Permission.Permisson.AccessToRequestCourse)]
        [HttpGet("GetPersonelByDepartman")]
        public IActionResult GetPersonelByDepartman(int departmanId)
        {
            var users = _personelRepository.GetDepartmansPersonel(departmanId).Result;
            if (users.Count() == 0) return NoContent();
            return Ok(users);
        }

        [Authorize(Permission.Permisson.AccessToReadPersonels)]
        [HttpGet("GetDepartmanManager")]
        public IActionResult GetDepartmanManager()
        {
            var user = _personelRepository.GetDepartmanManager().Result;
            if (user == null) return NoContent();
            return Ok(user);
        }

        [Authorize(Permission.Permisson.AccessToAddPersonel)]
        [HttpPut("UpdatePersonel")]
        public IActionResult UpdatePersonel([FromForm] UserPost userPost)
        {
            userPost.RoleId = (userPost.IsProfessor) ? 3 : 4;
            userPost.RoleId = (userPost.IsAdmin) ? 1 : userPost.RoleId;
            userPost.Permissions = (userPost.RoleId != 1) ? null : userPost.Permissions;
            string[] extentions = { "jpg", "jpeg" };
            userPost.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            userPost.DoctorExpert = (userPost.IsDoctor == false) ? null : userPost.DoctorExpert;
            userPost.ProfessorCourses = (userPost.IsProfessor == false) ? null : userPost.ProfessorCourses;
            string msg = null;
            if (userPost.Image != null)
            {
                string fileExt = userPost.Image.FileName.Split(".").Last().ToLower();
                userPost.Image = (Array.Exists(extentions, el => el == fileExt)) ? userPost.Image : null;
            }
            int res = _personelRepository.Update(userPost).Result;

            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddPersonel)]
        [HttpDelete("DeletePersonel")]
        public IActionResult DeletePersonel(long id)
        {
            int res = _personelRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
