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
        public IActionResult Index()
        {
            return View();
        }

        //register screen
        public IActionResult Signup()
        {
            return View();
        }

        //error screen
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> Authenticate(LoginData data)
        {
            var user = await UserService.Authenticate(data.UserName, data.Password);
            if (user == null)
                return Redirect("/login");
            return Redirect("/welcome");
        }

        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> Register(RegisterUser data)
        {
            data.Roles = new List<string>() { "Guest" };

            var user = await UserService.Register(data, this.ModelState);
            if (ModelState.IsValid)
                return Redirect("/login/welcome");

            // not enough to catch errors, do a try catch with a redirect
            //return Redirect("/login/error");
            return Ok(); // temp return for linter
        }


        public IActionResult Welcome()
        {
            return View();
        }

        [Authorize]
        //[Authorize(Roles="Admin")]
        //[Authorize(Policy="read")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
