using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using WebApi.FIlter;
using WebApi.Helper;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadGroups)]
        [HttpGet("GetGroups")]
        public IActionResult GetGroups()
        {
            var groups = _groupRepository.GetAll().Result;
            if (groups.Count() == 0) return NoContent();
            return Ok(groups);
        }

        [Authorize(Permission.Permisson.AccessToReadGroups)]
        [HttpGet("GetGroupById")]
        public IActionResult GetGroupById(int id)
        {
            var group = _groupRepository.Get(id).Result;
            if (group == null) return NoContent();
            return Ok(group);
        }

        [Authorize(Permission.Permisson.AccessToAddGroup)]
        [HttpPost("AddGroup")]
        public IActionResult AddGroup(Group group)
        {
            group.RegUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _groupRepository.Insert(group).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddGroup)]
        [HttpPut("UpdateGroup")]
        public IActionResult UpdateGroup(Group group)
        {
            group.EditUser = User.FindFirst(ClaimTypes.Name)?.Value;
            int res = _groupRepository.Update(group).Result;
            return Ok(res);

        }

        [Authorize(Permission.Permisson.AccessToAddGroup)]
        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteGroup(long id)
        {
            int res = _groupRepository.Delete(id).Result;
            return Ok(res);
        }
    }
}
