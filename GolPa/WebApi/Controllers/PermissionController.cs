using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : Controller
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        [HttpGet("GetPermissions")]
        public IActionResult GetPermissions()
        {
            var groups = _permissionRepository.GetAll().Result;
            return Ok(groups);
        }

        [HttpGet("GetPermissionById")]
        public IActionResult GetPermissionById(int id)
        {
            var forRes = new ForReturn();
            try
            {
                var user = _permissionRepository.Get(id).Result;
                return Ok(user);
            }
            catch (Exception)
            {
                forRes.ResCode = -1;
                forRes.Message = "دسترسی با چنین آیدی وجود ندارد !";
                return BadRequest(forRes);
            }

        }
    }
}
