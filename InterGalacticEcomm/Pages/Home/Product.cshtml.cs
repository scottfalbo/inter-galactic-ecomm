using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class ProductModel : PageModel
    { 
          
        public IAdmin _context;
        public ProductModel(IAdmin context)
        {
            _context = context;
        }

        public int Id;
        public string Name { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public async Task OnGet(int id)
        {
            Id = id;
            var product = await _context.GetProduct(id);
            Name = product.Name;
            Price = product.Price;
            URL = product.URL;
            Description = product.Description;
            Quantity = product.Quantity;
        }

        public async Task<IActionResult> OnPostAsync(int Id)
        {
            //var user = User.Claims.First(x => x.Type == "Id").Value;
            string id = HttpContext.Request.Cookies["user id"];
            var cart = await _context.GetCart(id);
            await _context.AddProductToCart(cart.Id, Id, Quantity);

            return Redirect("/Home/ShoppingCart");
        }


    }
}
