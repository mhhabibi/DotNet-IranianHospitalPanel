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
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadJobs)]
        [HttpGet("GetJobs")]
        public IActionResult GetJobs()
        {
            var jobs = _jobRepository.GetAll().Result;
            if (jobs.Count() == 0) return NoContent();
            return Ok(jobs);
        }

        [Authorize(Permission.Permisson.AccessToReadJobs)]
        [HttpGet("GetJobById")]
        public IActionResult GetJobById(int id)
        {
            var job = _jobRepository.GetById(id).Result;
            if (job == null) return NoContent();
            return Ok(job);
        }

        [Authorize(Permission.Permisson.AccessToAddJob)]
        [HttpPost("AddJob")]
        public IActionResult AddJob(Job job)
        {
            job.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _jobRepository.Insert(job).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddJob)]
        [HttpPut("UpdateJob")]
        public IActionResult UpdateJob(Job job)
        {
            job.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _jobRepository.Update(job).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddJob)]
        [HttpDelete("DeleteJob")]
        public IActionResult DeleteJob(long id)
        {
            int res = _jobRepository.Delete(id).Result;
            return Ok(res);

        }
    }
}
