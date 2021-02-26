using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterGalacticEcomm.Data;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterGalacticEcomm.Pages.Home
{
    public class IndexModel : PageModel
    {
        public IAdmin _context;
        public List<Category> CategoryList { get; set; }
        [BindProperty]
        public string Username { get; set; }
        public IndexModel(IAdmin context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            //Display all the categories
            string userName = HttpContext.Request.Cookies["user name"];
            Username = userName;
            CategoryList = await _context.GetCategories();
        }

    }
}
