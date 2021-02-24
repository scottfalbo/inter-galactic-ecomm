using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class ShoppingCartModel : PageModel
    {
        public IAdmin _context;
        public ShoppingCartModel(IAdmin context)
        {
            _context = context;
        }

        public string UserName { get; set; }
        public List<CartProducts> CartProducts { get; set; }
        public int Id { get; set; }


        public async Task OnGet()
        {
            string user = HttpContext.Request.Cookies["user name"];
            string id = HttpContext.Request.Cookies["user id"];
            UserName = user;
            var cart = await _context.GetCart(id);
            CartProducts = cart.CartProducts;
        }
    }

}
