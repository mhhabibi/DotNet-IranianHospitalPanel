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
    public class ExpertController : Controller
    {
        private readonly IExpertRepository _expertRepository;
        public ExpertController(IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadDepartmans)]
        [HttpGet("GetExperts")]
        public IActionResult GetExperts()
        {
            var groups = _expertRepository.GetAll().Result;
            if (groups.Count() == 0) return NoContent();
            return Ok(groups);
        }

        [Authorize(Permission.Permisson.AccessToReadExperts)]
        [HttpGet("GetExpertById")]
        public IActionResult GetExpertById(int id)
        {
            var expert = _expertRepository.Get(id).Result;
            if (expert == null) return NoContent();
            return Ok(expert);
        }

        [Authorize(Permission.Permisson.AccessToAddExpert)]
        [HttpPost("AddExpert")]
        public IActionResult AddExpert(Expert expert)
        {
            expert.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _expertRepository.Insert(expert).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddExpert)]
        [HttpPut("UpdateExpert")]
        public IActionResult UpdateExpert(Expert expert)
        {
            expert.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _expertRepository.Update(expert).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddExpert)]
        [HttpDelete("DeleteExpert")]
        public IActionResult DeleteExpert(long id)
        {
            int res = _expertRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
