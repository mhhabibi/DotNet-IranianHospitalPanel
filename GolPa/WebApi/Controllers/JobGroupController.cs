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
    public class JobGroupController : Controller
    {
        private readonly IJobGroupRepository _jobGroupRepository;
        public JobGroupController(IJobGroupRepository jobGroupRepository)
        {
            _jobGroupRepository = jobGroupRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadJobGroups)]
        [HttpGet("GetJobGroups")]
        public IActionResult GetJobGroups()
        {
            var jobGroups = _jobGroupRepository.GetAll().Result;
            if (jobGroups.Count() == 0) return NoContent();
            return Ok(jobGroups);
        }

        [Authorize(Permission.Permisson.AccessToReadJobGroups)]
        [HttpGet("GetJobGroupById")]
        public IActionResult GetJobGroupById(int id)
        {
            var jobGroup = _jobGroupRepository.GetById(id).Result;
            if (jobGroup == null) return NoContent();
            return Ok(jobGroup);
        }

        [Authorize(Permission.Permisson.AccessToAddJobGroup)]
        [HttpPost("AddJobGroup")]
        public IActionResult AddJobGroup(JobGroup jobGroup)
        {
            jobGroup.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _jobGroupRepository.Insert(jobGroup).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddJobGroup)]
        [HttpPut("UpdateJobGroup")]
        public IActionResult UpdateDepartman(JobGroup jobGroup)
        {
            jobGroup.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _jobGroupRepository.Update(jobGroup).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddJobGroup)]
        [HttpDelete("DeleteJobGroup")]
        public IActionResult DeleteJobGroup(long id)
        {
            int res = _jobGroupRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
