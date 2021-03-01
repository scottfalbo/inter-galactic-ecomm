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
    public class ThankYouModel : PageModel
    {
        public IAdmin _context;
        public ThankYouModel(IAdmin context)
        {
            _context = context;
        }

        public List<CartProducts> cartProducts { get; set; }
        public string Username { get; set; }
        public decimal TotalPrice { get; set; }
        public async Task OnGet()
        {
            string id = HttpContext.Request.Cookies["user id"];
            string userName = HttpContext.Request.Cookies["user name"];
            var order = await _context.GetOrder(id);

            cartProducts = order.Cart.CartProducts;

            foreach (var item in order.Cart.CartProducts)
            {
                TotalPrice += item.Product.Price;
            }

            Username = userName;

            await _context.EmptyCart(order.Cart);
        }
    }
}
