using InterGalacticEcomm.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services
{
    public class AdminRepository : IAdmin
    {
        private readonly GalacticDbContext _context;
        public AdminRepository(GalacticDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category category)
        {
            Category cat = new Category()
            {
                CategoryName = category.CategoryName,
                Description = category.Description,
            };
            _context.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task CreateProduct(Product product)
        {
            Product prod = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category
            };
            _context.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();
        }
        public async Task<Category> GetCategory(int Id)
        {
            return await _context.Categories
                .Where(x => x.Id == Id)
                .Select(z => new Category
                {
                    CategoryName = z.CategoryName,
                    Description = z.Description,
                    CategoryProducts = z.CategoryProducts
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetProduct(int Id)
        {
            return await _context.Products
                .Where(x => x.Id == Id)
                .Select(z => new Product
                {
                    Name = z.Name,
                    Description = z.Description,
                    Price = z.Price,
                    Category = z.Category
                })
                .FirstOrDefaultAsync();
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories
                .Select(z => new Category
                {
                    Id = z.Id,
                    CategoryName = z.CategoryName,
                    Description = z.Description,
                    CategoryProducts = z.CategoryProducts
                })
                .ToListAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products
                .Select(z => new Product
                {
                    Id = z.Id,
                    Name = z.Name,
                    Description = z.Description,
                    Price = z.Price,
                    Category = z.Category
                })
                .ToListAsync();
        }

        //TODO: This is where shit breaks
        public async Task AddProductToCategory(int categoryId, int productId)
        {
            CategoryProduct categoryProduct = new CategoryProduct()
            {
                ProductId = productId,
                CategoryId = categoryId,
            };
            _context.Entry(categoryProduct).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductFromCategory(int categoryId, int productId)
        {
            var categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.ProductId == productId);
            _context.Entry(categoryProduct).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(int Id, Category category)
        {
            Category cat = new Category()
            {
                Id = Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            _context.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(int Id, Product product)
        {
            Product prod = new Product()
            {
                Id = Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category
            };
            _context.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int Id)
        {
            Category cat = await _context.Categories.FindAsync(Id);
            _context.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int Id)
        {
            Product product = await _context.Products.FindAsync(Id);
            _context.Entry(product).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }



    }
}
