using InterGalacticEcomm.Models;
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
        /// <summary>
        /// Home Page
        /// </summary>
        /// <returns> Index View() </returns>
        public IActionResult Index()
        {
            List<Category> categoriesList = GetCategories();
            return View(categoriesList);
        }

        /// <summary>
        /// Get a single category by ID from the DB and send to view
        /// </summary>
        /// <param name="id"> Category Id </param>
        /// <returns> Category View() </returns>
        [HttpGet]
        public IActionResult Category(string categoryName, string description) //this will be int id
        {
            List<Product> productList = GetProducts();

            CategoryProductVM productVM = new CategoryProductVM()
            {
                Category = new Category { CategoryName = categoryName, Description = description },
                Product = productList,
            };

            return View(productVM);
        }

        /// <summary>
        /// Get a list of all of categories and send them to the view
        /// </summary>
        /// <returns> Categories View() </returns>
        [HttpGet]
        public IActionResult Categories()
        {
            List<Category> categoriesList = GetCategories();

            return View(categoriesList);
        }

        /// <summary>
        /// Get a Product by ID and send it to the view
        /// </summary>
        /// <param name="id"> Product Id </param>
        /// <returns> Product View() </returns>
        [HttpGet]
        public IActionResult Product(string Name)
        {
            Product product = new Product() {Name = Name };

            return View(product);
        }

        /// <summary>
        /// Creates and saves a new Category object in the DB
        /// </summary>
        /// <returns> Newly created Category object </returns>
        [HttpPost]
        [Authorize]
        [Authorize(Roles="Admin")]
        public async Task<Category> CreateCategory()
        {
            return new Category();
        }

        /// <summary>
        /// Creates and saves a new Product object in the DB
        /// </summary>
        /// <returns> Newly created Product object </returns>
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<Product> CreateProduct()
        {
            return new Product();
        }

        /// <summary>
        /// Update and save the data on a Catergory by Id
        /// </summary>
        /// <param name="id"> Category Id </param>
        /// <returns> Category View() </returns>
        [HttpPut]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<Category> UpdateCategory(int id)
        {
            return new Category();
        }

        /// <summary>
        /// Udpate and save the data on a Product by Id
        /// </summary>
        /// <param name="id"> Product Id </param>
        /// <returns> Product View() </returns>
        [HttpPut]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<Product> UpdateProduct(int id)
        {
            return new Product();
        }

        /// <summary>
        /// Delete a Category by Id
        /// </summary>
        /// <param name="id"> Category Id </param>
        [HttpDelete]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task DeleteCategory(int id)
        {

        }

        /// <summary>
        /// Delete a Product by Id
        /// </summary>
        /// <param name="id"> Product Id </param>
        [HttpDelete]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task DeleteProduct(int id)
        {

        }

        /// <summary>
        /// Helper methods for making dummy data, going away soon once DB is hooked up
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            List<Product> productList = new List<Product>()
            {
                new Product(){Name = "PlumbusA", Id = 1, Description = "This is A", Price = 11.25m},
                new Product(){Name = "PlumbusB", Id = 2, Description = "This is B", Price = 100.25m},
                new Product(){Name = "PlumbusC", Id = 3, Description = "This is C", Price = 1.98m},
            };
            return productList;
        }
        public List<Category> GetCategories()
        {
            List<Category> categoriesList = new List<Category>()
            {
                new Category(){CategoryName = "Category 1", Id = 1, Description = "This category will display ALL category 1 items"},
                new Category(){CategoryName = "Category 2", Id = 2, Description = "This category will display ALL category 2 items, such interactive"}
            };
            return categoriesList;
        }
    }
}
