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

        /// <summary>
        /// Home page, currently our login screen.  Calls /authenticate
        /// </summary>
        /// <returns> Index View() </returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// New user registration page.  Uses a form that calls /register
        /// </summary>
        /// <returns> Signup View() </returns>
        [AllowAnonymous]
        public IActionResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Error screen to redirect to if things go wrong
        /// </summary>
        /// <returns> Error View() </returns>
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Authentication route for the login screen
        /// </summary>
        /// <param name="data"> LoginData object with username and password from form </param>
        /// <returns> Redirect to appropriate page </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AppUserDTO>> Authenticate(LoginData data)
        {
            var user = await UserService.Authenticate(data.UserName, data.Password);
            if (user == null)
                return Redirect("/login");
            return Redirect("/login/welcome");
        }

        /// <summary>
        /// Route used by /Signup to register a new user 
        /// </summary>
        /// <param name="data"> RegisterUser object with form input </param>
        /// <returns> Redirect() </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AppUserDTO>> Register(RegisterUser data)
        {
            //data.Roles = new List<string>() { "Guest" };

            var user = await UserService.Register(data, this.ModelState);
            if (user != null)
                return Redirect("/login/");

            // not enough to catch errors, do a try catch with a redirect
            //return Redirect("/login/error");
            throw new Exception("womp womp"); // temp return for linter
        }

        /// <summary>
        /// Currently a generic welcome redirect page after a successful login
        /// </summary>
        /// <returns> Welcome View() </returns>
        [Authorize]
        public IActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// Holder page for user profiles, currently only accessable by admins
        /// </summary>
        /// <returns> Profile View() </returns>
        [Authorize]
        [Authorize(Roles="Admin")]
        //[Authorize(Policy="read")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
