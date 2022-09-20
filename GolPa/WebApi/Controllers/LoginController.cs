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
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("GetUser")]
        public IActionResult Index(AuthenticateRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRepository.Authenticate(model.Username, model.Password);

                if (response == null)
                    return BadRequest(new { message = "نام کاربری یا رمز عبور اشتباه است !" });

                return Ok(response.Token);
            }
            else
                return BadRequest(new { message = "نام کاربری و رمز عبور را وارد کنید ! "});

        }
    }
}
