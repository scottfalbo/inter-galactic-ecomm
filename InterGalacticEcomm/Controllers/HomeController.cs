using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdmin _context;
        public HomeController(IAdmin context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _context.GetOrders();
            return View(orders);
        }
    }
}
