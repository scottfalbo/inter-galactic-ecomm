using InterGalacticEcomm.Models.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<AppUser> UserManager;

        public IdentityUserService(UserManager<AppUser> registerUser)
        {
            UserManager = registerUser;
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
            var user = await UserManager.FindByNameAsync(userName);

            if (await UserManager.CheckPasswordAsync(user, password))
            {

                return new AppUserDTO
                {
                    UserName = user.UserName,
                    Id = user.Id
                };
            }

            throw new Exception("womp womp");
        }
        
    }
}
