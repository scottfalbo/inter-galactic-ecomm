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
        private readonly UserManager<AppUserDTO> userManager;

        public IdentityUserService(UserManager<AppUserDTO> registerUser)
        {
            userManager = registerUser;
        }

        public async Task<AppUserDTO> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new AppUserDTO()
            {
                UserName = data.UserName,
                Password = data.Password
            };
            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                //beacuse they are a user, add their role
                await userManager.AddToRolesAsync(user, data.Roles);

                return new AppUserDTO
                {
                    UserName = user.UserName

                };
            }
            return new AppUserDTO();
        }

        public async Task<AppUserDTO> Authenticate(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (await userManager.CheckPasswordAsync(user, password))
            {

                return new AppUserDTO
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Roles = user.Roles
                };
            }

            return null;
        }
        //TODO: add authenticate method
    }
}
