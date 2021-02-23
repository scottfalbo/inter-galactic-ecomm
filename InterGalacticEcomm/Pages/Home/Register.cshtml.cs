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
    public class RegisterModel : PageModel
    {
        public IUserService service { get; }
        public RegisterModel(IUserService Service)
        {
            service = Service;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string Username, string Password)
        {
            //Register the appuser
            RegisterUser newUser = new RegisterUser()
            {
                UserName = Username,
                Password = Password
            };

            await service.Register(newUser, this.ModelState);
            return Redirect("/Home/Login");
        }
    }
}
