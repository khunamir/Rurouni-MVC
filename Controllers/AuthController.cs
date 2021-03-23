using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rurouni_v2.Services;
using Rurouni_v2.Models.DTO.Responses;
using Rurouni_v2.Models.DTO.Requests;

namespace Rurouni_v2.Controllers
{
    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Get - Index
        public IActionResult Index()
        {
            return View();
        }

        // Get - Register
        public IActionResult Register()
        {
            return View();
        }

        // Post - Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.isSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Invalid payload");
        }

        // Get - Login
        public IActionResult Login()
        {
            return View();
        }

        // Post - Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.isSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Invlaid payload");
        }
    }
}
