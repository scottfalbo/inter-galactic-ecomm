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
    public class CategoryModel : PageModel
    {
        public IAdmin _context;
        public CategoryModel(IAdmin context)
        {
            _context = context;
        }
        public int Id;
        public List<CategoryProduct> CategoryProductList { get; set; }
        public string CategoryName { get; set; }
        public async Task OnGet(int id)
        {
            Id = id;
            var category = await _context.GetCategory(Id);
            CategoryName = category.CategoryName;
            CategoryProductList = category.CategoryProducts;
        }
    }
}
