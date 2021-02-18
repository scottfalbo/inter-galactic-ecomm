using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _admin.GetCategories();
            return View(categories);
        }
        public async Task<IActionResult> Category(int Id) //this will be int id
        {
            var category = await _admin.GetCategory(Id);
            CategoryProductVM catVM = new CategoryProductVM()
            {
                Category = category,
                Product = await _admin.GetProducts()
            };

            if (category == null)
            {
                return NotFound();
            }
            return View(catVM);
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _admin.GetCategories();

            CategoryListVM categoryVM = new CategoryListVM()
            {
                Category = new Category(),
                CategoryList = categories
            };

            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Product(int Id)
        {
            var product = await _admin.GetProduct(Id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //We don't need Products() at the moment. We are basically using this in our category.cshtml
        public async Task<IActionResult> Products()
        {
            var products = await _admin.GetProducts();

            ProductListVM newList = new ProductListVM()
            {
                Product = new Product(),
                ProductList = products
            };

            return View(newList);
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Category(Category category)
        {
            await _admin.CreateCategory(category);
            Category newCat = await _admin.GetCategory(category.Id);
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //string text = "Sucess";
            //MessageBox.show(text);
            //return Content("You made a category!");

            return Redirect("/Admin/Categories");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Product(Product product)
        {
            await _admin.CreateProduct(product);
            Product newProd = await _admin.GetProduct(product.Id);
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            //return Content("You made a product!");
            //make a message box or something here, nice job!

            return Redirect("/Admin/Products");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            await _admin.UpdateCategory(category.Id, category);

            return Redirect("/Admin/Categories");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _admin.UpdateProduct(product.Id, product);

            return Redirect("/Admin/Products");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            await _admin.DeleteCategory(Id);

            return Redirect("/Admin/Categories");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            await _admin.DeleteProduct(Id);

            return Redirect("/Admin/Products");
        }
    }
}
