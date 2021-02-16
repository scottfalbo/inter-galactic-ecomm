using InterGalacticEcomm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Category> categoriesList = GetCategories();
            return View(categoriesList);
        }

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

        //We are using the Categories() on the index page so the user is able to navigate. Categories() is useless at the moment.
        public IActionResult Categories()
        {
            List<Category> categoriesList = GetCategories();

            return View(categoriesList);
        }


        public IActionResult Product(string Name)
        {
            Product product = new Product() {Name = Name };

            return View(product);
        }

        //We don't need Products() at the moment. We are basically using this in our category.cshtml
        public IActionResult Products()
        {
            List<Product> productList = GetProducts();

            return View(productList);
        }

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
