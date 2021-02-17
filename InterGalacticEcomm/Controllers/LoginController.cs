using InterGalacticEcomm.Models.API;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Controllers
{
    public class LoginController : Controller
    {
        private IUserService UserService { get; }

        public LoginController (IUserService service)
        {
            UserService = service;
        }

        //login screen
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //register screen
        [AllowAnonymous]
        public IActionResult Signup()
        {
            return View();
        }

        //error screen
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AppUserDTO>> Authenticate(LoginData data)
        {
            var user = await UserService.Authenticate(data.UserName, data.Password);
            if (user == null)
                return Redirect("/login");
            return Redirect("/login/welcome");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AppUserDTO>> Register(RegisterUser data)
        {
            data.Roles = new List<string>() { "Guest" };

            var user = await UserService.Register(data, this.ModelState);
            if (user != null)
                return Redirect("/login/");

            // not enough to catch errors, do a try catch with a redirect
            //return Redirect("/login/error");
            throw new Exception("womp womp"); // temp return for linter
        }

        [Authorize]
        public IActionResult Welcome()
        {
            return View();
        }

        [Authorize]
        [Authorize(Roles="Admin")]
        //[Authorize(Policy="read")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
