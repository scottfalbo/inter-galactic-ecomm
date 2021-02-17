using InterGalacticEcomm.Models.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<AppUser> UserManager;
        private SignInManager<AppUser> signInManager;

        public IdentityUserService(UserManager<AppUser> registerUser, SignInManager<AppUser> sim)
        {
            UserManager = registerUser;
            signInManager = sim;
        }

        public async Task<AppUserDTO> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new AppUser()
            {
                UserName = data.UserName,
                Email = data.Email
            };
            var result = await UserManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                //beacuse they are a user, add their role
                await UserManager.AddToRolesAsync(user, data.Roles);

                return new AppUserDTO
                {
                    UserName = user.UserName,
                    Roles = new List<string>() { "Guest" }
                };
            }
            return new AppUserDTO();
        }

        public async Task<AppUserDTO> Authenticate(string userName, string password)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, true, false);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(userName);
                return new AppUserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = await UserManager.GetRolesAsync(user)
                };
            }
            throw new Exception("womp womp");
        }

        public async Task<AppUserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await UserManager.GetUserAsync(principal);
            return new AppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = await UserManager.GetRolesAsync(user)
            };
        }
    }
}
