using InterGalacticEcomm.Data;
using InterGalacticEcomm.Models.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<AppUser> signInManager;
        private readonly GalacticDbContext _context;

        public IdentityUserService(UserManager<AppUser> registerUser, SignInManager<AppUser> sim, GalacticDbContext context)
        {
            UserManager = registerUser;
            signInManager = sim;
            _context = context;
        }

        /// <summary>
        /// Method to register a new user and assign them a role of "Guest".
        /// </summary>
        /// <param name="data"> RegisterUser object with input from form via the controller </param>
        /// <param name="modelState"> ModelStateDictionary object, magic </param>
        /// <returns> new AppUserDTO object </returns>
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
                var cart = await CreateCart(user.Id);
                await UserManager.AddToRolesAsync(user, new List<string>() { "Guest" });

                return new AppUserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = new List<string>() { "Guest" },
                };
            }
            return new AppUserDTO();
        }

        /// <summary>
        /// Method used by login to authenticate users.  
        /// </summary>
        /// <param name="userName"> username from form input via the controller </param>
        /// <param name="password"> password from form input via the controller </param>
        /// <returns> AppUserDTO object </returns>
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
                    Email = user.Email,
                    Roles = await UserManager.GetRolesAsync(user),
                };
            }
            throw new Exception("womp womp");
        }

        /// <summary>
        /// Not really sure yet
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Make a new a cart when user registers
        /// </summary>
        /// <param name="Id"> user Id</param>
        /// <returns> Cart object </returns>
        public async Task<Cart> CreateCart(string Id)
        {
            Cart cart = new Cart()
            {
                UserId = Id,
                CartProducts = new List<CartProducts>()
            };
            _context.Entry(cart).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
