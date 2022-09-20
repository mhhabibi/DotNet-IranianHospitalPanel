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
    public class IllnessController : Controller
    {
        private readonly IIllnessRepository _illnessRepository;
        public IllnessController(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadIllnesses)]
        [HttpGet("GetIllnesses")]
        public IActionResult GetIllnesses()
        {
            var illnesses = _illnessRepository.GetAll().Result;
            if (illnesses.Count() == 0) return NoContent();
            return Ok(illnesses);
        }

        [Authorize(Permission.Permisson.AccessToReadIllnesses)]
        [HttpGet("GetIllnessById")]
        public IActionResult GetIllnessById(int id)
        {
            var illness = _illnessRepository.Get(id).Result;
            if (illness == null) return NoContent();
            return Ok(illness);
        }

        [Authorize(Permission.Permisson.AccessToAddIllness)]
        [HttpPost("AddIllness")]
        public IActionResult AddIllness(Illness illness)
        {
            illness.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _illnessRepository.Insert(illness).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddIllness)]
        [HttpPut("UpdateIllness")]
        public IActionResult UpdateIllness(Illness illness)
        {
            illness.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _illnessRepository.Update(illness).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddIllness)]
        [HttpDelete("DeleteIllness")]
        public IActionResult DeleteIllness(long id)
        {
            int res = _illnessRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
