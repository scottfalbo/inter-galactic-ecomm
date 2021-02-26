using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models.API;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class LoginModel : PageModel
    {
        public IUserService service { get; }
        public LoginModel(IUserService Service)
        {
            service = Service;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        [BindProperty]
        public int cartCount { get; set; }
        public void OnGet()
        {
            //cartCount = Convert.ToInt32(HttpContext.Request.Cookies["CartCount"]);
        }
        public async Task<IActionResult> OnPostAsync(string Username, string Password)
        {
            //Authenticate the appuser
            LoginData newUser = new LoginData()
            {
                UserName = Username,
                Password = Password
            };

            var user = await service.Authenticate(newUser.UserName, newUser.Password);

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            HttpContext.Response.Cookies.Append("user Id", user.Id, cookieOptions);
            HttpContext.Response.Cookies.Append("user name", user.UserName, cookieOptions);
            HttpContext.Response.Cookies.Append("user email", user.Email, cookieOptions);

            return Redirect("/Home/Index");
        }
    }
}
