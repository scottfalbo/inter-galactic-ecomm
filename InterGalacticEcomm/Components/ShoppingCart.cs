using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Components
{
    [ViewComponent]
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IAdmin admin;
        public ShoppingCartViewComponent(IAdmin Admin)
        {
            admin = Admin;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string UserId = HttpContext.Request.Cookies["user Id"];
            Cart cart = await admin.GetCart(UserId);

            ViewModel VM = new ViewModel()
            {
                CartCount = cart.CartProducts.Count()
            };

            return View(VM);
        }

        public class ViewModel 
        {
            public int CartCount { get; set; }
        }

    }
}
