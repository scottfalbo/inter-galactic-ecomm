using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class CheckoutModel : PageModel
    {

        public IAdmin _context;
        public CheckoutModel(IAdmin context)
        {
            _context = context;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        [BindProperty]
        public Cart Cart { get; set; }
        public bool Shipped { get; set; }
        public bool Recieved { get; set; }
        public bool Paid { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }

        public async Task OnGet()
        {
            string id = HttpContext.Request.Cookies["user id"];

            var order = await _context.GetOrder(id);

            UserId = order.UserId;
            Cart = order.Cart;
            Paid = order.Paid;
            Shipped = order.Shipped;
            Paid = order.Paid;
            
            foreach (var item in order.Cart.CartProducts)
            {
                TotalPrice += item.Product.Price;
            }

        }

    }
}
