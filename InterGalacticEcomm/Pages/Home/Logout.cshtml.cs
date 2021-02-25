using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> signInManager;

        public LogoutModel(SignInManager<AppUser> sim)
        {
            signInManager = sim;
        }
        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
