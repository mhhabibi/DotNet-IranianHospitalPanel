using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
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
    public class DepartmanController : Controller
    {
        private readonly IDepartmanRepository _departmanRepository;
        public DepartmanController(IDepartmanRepository departmanRepository)
        {
            _departmanRepository = departmanRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadDepartmans)]
        [HttpGet("GetDepartmans")]
        public IActionResult GetDepartmans()
        {
            var departmans = _departmanRepository.GetAll().Result;
            if (departmans.Count() == 0) return NoContent();
            return Ok(departmans);
        }

        [Authorize(Permission.Permisson.AccessToReadDepartmans)]
        [HttpGet("GetDepartmanById")]
        public IActionResult GetDepartmanById(int id)
        {
            var departman = _departmanRepository.GetById(id).Result;
            if (departman == null) return NoContent();
            return Ok(departman);

        }

        [Authorize(Permission.Permisson.AccessToReadDepartmans)]
        [HttpGet("GetDepartmanCourses")]
        public IActionResult GetDepartmanCourses(int departmanId)
        {
            var departman = _departmanRepository.GetDepartmanCourses(departmanId).Result;
            if (departman.ToList().Count == 0) return NoContent();
            return Ok(departman);

        }


        [Authorize(Permission.Permisson.AccessToAddDepartman)]
        [HttpPost("AddDepartman")]
        public IActionResult AddDepartman(Departman departman)
        {
            departman.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _departmanRepository.Insert(departman).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddDepartman)]
        [HttpPut("UpdateDepartman")]
        public IActionResult UpdateDepartman(Departman departman)
        {
            departman.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _departmanRepository.Update(departman).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddDepartman)]
        [HttpDelete("DeleteDepartman")]
        public IActionResult DeleteDepartman(long id)
        {
            int res = _departmanRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
