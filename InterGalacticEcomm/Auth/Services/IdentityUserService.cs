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
        private readonly UserManager<AppUserDTO> UserManager;

        public IdentityUserService(UserManager<AppUserDTO> registerUser)
        {
            UserManager = registerUser;
        }

        public async Task<AppUserDTO> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new AppUserDTO()
            {
                UserName = data.UserName,
                Password = data.Password
            };
            var result = await UserManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                //beacuse they are a user, add their role
                await UserManager.AddToRolesAsync(user, data.Roles);

                return new AppUserDTO
                {
                    UserName = user.UserName

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
                    Password = user.Password,
                    Roles = "Guest"
                };
            }

            return null;
        }
        
    }
}
