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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string cartCount = HttpContext.Request.Cookies["CartCount"];

            ViewModel cart = new ViewModel()
            { 
                CartCount = Convert.ToInt32(cartCount)
            };

            return View(cart);
        }

        public class ViewModel 
        {
            public int CartCount { get; set; }
        }

    }
}
