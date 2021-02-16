using InterGalacticEcomm.Models.API;
using InterGalacticEcomm.Models.Interface.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface
{
    public interface IUserService
    {
        public Task<AppUserDTO> Register(RegisterUser data, ModelStateDictionary modelState);

        public Task<AppUserDTO> Authenticate(string userName, string password);
    }
}