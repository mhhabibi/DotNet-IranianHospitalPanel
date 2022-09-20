using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EducationalCalendarController : Controller
    {
        private readonly IEducationalCalendarRepository _educationalCalendarRepository;
        public EducationalCalendarController(IEducationalCalendarRepository educationalCalendarRepository)
        {
            _educationalCalendarRepository = educationalCalendarRepository;
        }

        [Authorize(Policy = "AccessToReadEducationalCalender")]
        [HttpGet("GetEducationalCalendar")]
        public IActionResult GetEducationalCalendar()
        {
            int i = 134;
            bool hasPermission = false;
            string curUser = User.FindFirst(ClaimTypes.Name)?.Value;
            if (User.HasClaim("AccessToReadEducationalCalenderAsAdmin", "true"))
                hasPermission = true;
            else if (User.HasClaim("AccessToReadEducationalCalenderAsDepartman", "true"))
                hasPermission = false;
            var jobGroups = _educationalCalendarRepository.GetEducationCalender(hasPermission,Int32.Parse(curUser)).Result;
            var forRes = new ForReturn();
            forRes.ResCode = 1;
            forRes.Message = "با موفقیت اجرا شد";
            forRes.Info = jobGroups;
            return Ok(jobGroups);
        }


        [HttpGet("GetEducationalCalendarByDate")]
        public IActionResult GetEducationalCalendarByDate(string fromDate,string toDate)
        {
            bool hasPermission = false;
            string curUser = User.FindFirst(ClaimTypes.Name)?.Value;
            if (User.HasClaim("AccessToReadEducationalCalenderAsAdmin", "true"))
                hasPermission = true;
            else if (User.HasClaim("AccessToReadEducationalCalenderAsDepartman", "true"))
                hasPermission = false;
            var jobGroups = _educationalCalendarRepository.GetEducationCalenderByDate(hasPermission, Int32.Parse(curUser),fromDate,toDate).Result;
            var forRes = new ForReturn();
            forRes.ResCode = 1;
            forRes.Message = "با موفقیت اجرا شد";
            forRes.Info = jobGroups;
            return Ok(jobGroups);
        }
    }
}
