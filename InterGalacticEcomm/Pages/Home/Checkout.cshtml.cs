using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using InterGalacticEcomm.Models.Interface.Services.Authorize.Interfaces;
using InterGalacticEcomm.Models.Interface.Services.Authorize.Models;
using InterGalacticEcomm.Models.Interface.Services.Email.Interfaces;
using InterGalacticEcomm.Models.Interface.Services.Email.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class CheckoutModel : PageModel
    {

        public IAdmin _context;
        public IEmail _emailService;
        public IAuthorize _authorizeService;
        public CheckoutModel(IAdmin context, IEmail email, IAuthorize auth)
        {
            _context = context;
            _emailService = email;
            _authorizeService = auth;
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

        //public string status {get; set;}


        public async Task<IActionResult> OnPost()
        {
            // autrhorize card
            //if good send email
            if (_authorizeService.AuthorizeCard(new CreditCard()))    
            {
                string email = HttpContext.Request.Cookies["user email"];
                string userName = HttpContext.Request.Cookies["user name"];

                Message newMessage = new Message()
                {
                    To = email,
                    Subject = $"Thank you for your InterGalactic order {userName}!!!",
                    Body = "hello world"
                };
                Message adminMessage = new Message()
                {
                    To = "mattpet26@gmail.com",
                    Subject = "An order was palced with things",
                    Body = "some one bought stuff"
                };
                Message whMessage = new Message()
                {
                    To = "arcanumseattle@gmail.com",
                    Subject = $"{userName} placed an order",
                    Body = "go ship the stuff"
                };

                await _emailService.SendEmailAsync(newMessage);
                await _emailService.SendEmailAsync(adminMessage);
                await _emailService.SendEmailAsync(whMessage);

                return Redirect("/Home/ThankYou");
            }
            return Redirect("/Home/Error");
        }

    }
}
