using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models.API;
using InterGalacticEcomm.Models.Interface;
using InterGalacticEcomm.Models.Interface.Services.Email.Interfaces;
using InterGalacticEcomm.Models.Interface.Services.Email.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class RegisterModel : PageModel
    {
        public IEmail _emailService;
        public IUserService service { get; }
        public RegisterModel(IUserService Service, IEmail email)
        {
            service = Service;
            _emailService = email;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string Username, string Password, string Email)
        {
            //Register the appuser
            RegisterUser newUser = new RegisterUser()
            {
                UserName = Username,
                Password = Password,
                Email = Email
            };

            var user = await service.Register(newUser, this.ModelState);

            Message newMessage = new Message()
            {
                To = newUser.Email,
                Subject = $"Thank you signing up {newUser.UserName}",
                Body = $"We appreciate you creating an account with GalacticEcomm! "

            };

            await _emailService.SendEmailAsync(newMessage);
            return Redirect("/Home/Login");
        }
    }
}
