using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rurouni_v2.Services;
using Rurouni_v2.Models.DTO.Responses;
using Rurouni_v2.Models.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Rurouni_v2.Data;
using Microsoft.AspNetCore.Identity;

namespace Rurouni_v2.Controllers
{
    public class AuthController : Controller
    {
        private IUserService _userService;
        private readonly ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;

        public AuthController(IUserService userService, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _db = db;
            _userManager = userManager;
        }

        // Get - Register
        [HttpGet("/Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }

        // Post - Register
        [HttpPost("/Auth/Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.isSuccess)
                    return RedirectToAction("Auth", "Login", result);

                return BadRequest(result);
            }

            return BadRequest("Invalid payload");
        }

        // Get - Login
        [HttpGet("/Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        // Post - Login
        [HttpPost("/Auth/Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                var username = await _userManager.FindByNameAsync(model.Email);

                if (result.isSuccess)
                {
                    HttpContext.Session.SetString("JWToken", result.Message);
                    HttpContext.Session.SetString("Id", username.Id);
                }

                return Redirect("~/Journal/Index");
            }

            return BadRequest("Invalid payload");
        }
    }
}
