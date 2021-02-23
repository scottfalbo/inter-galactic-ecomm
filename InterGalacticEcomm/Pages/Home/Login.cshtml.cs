using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models.API;
using InterGalacticEcomm.Models.Interface;
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
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(string Username, string Password)
        {
            //Authenticate the appuser
            LoginData newUser = new LoginData()
            {
                UserName = Username,
                Password = Password
            };

            await service.Authenticate(newUser.UserName, newUser.Password);
            return Redirect("/Home/Index");
        }
    }
}
