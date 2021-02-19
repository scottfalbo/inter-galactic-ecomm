using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using InterGalacticEcomm.Models.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Controllers
{
    public class AdminController : Controller
    {
        //public IUploadService UploadService { get; }
        //public AdminController(IUploadService service)
        //{
            //UploadService = service;
        //}

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

        [HttpGet]
        public async Task<IActionResult> Category(int Id)
        {
            var category = await _admin.GetCategory(Id);
            CategoryProductVM catVM = new CategoryProductVM()
            {
                Category = new Category()
                {
                    Id = Id,
                    CategoryName = category.CategoryName,
                    CategoryProducts = category.CategoryProducts
                },
                Product = await _admin.GetProducts()
            };

            if (category == null)
            {
                return NotFound();
            }
            return View(catVM);
        }

        [HttpGet]
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
        [HttpGet]
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
            //Category newCat = await _admin.GetCategory(category.Id);
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            return Redirect("/Admin/Categories");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Product(Product product)
        {
            await _admin.CreateProduct(product);
            //Product newProd = await _admin.GetProduct(product.Id);
            if (!ModelState.IsValid)
            {
                return View(product);
            }

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
        [AllowAnonymous]
        public async Task<IActionResult> AddProductToCategory(int categoryId)
        {
            var productId = Convert.ToInt32(Request.Form["Product"]);
            //int prodId = ProductId.Value;
            await _admin.AddProductToCategory(categoryId, productId);
            return Redirect("/Admin/Category");
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateImage(IFormFile file)
        {
            //Product product = await UploadService.Upload(file);

            //pass in the obj created
            return View();
        }
    }
}
