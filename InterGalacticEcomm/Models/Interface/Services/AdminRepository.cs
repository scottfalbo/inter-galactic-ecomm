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
        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>no return</returns>
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

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>no return</returns>
        public async Task CreateProduct(Product product)
        {
            Product prod = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                URL = product.URL
            };
            _context.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Gets a single category by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>returns category by ID</returns>
        public async Task<Category> GetCategory(int Id)
        {
            return await _context.Categories
                .Where(x => x.Id == Id)
                .Include(q => q.CategoryProducts)
                .ThenInclude(a => a.Product)
                .Select(z => new Category
                {
                    CategoryName = z.CategoryName,
                    Description = z.Description,
                    CategoryProducts = z.CategoryProducts
                })
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// Gets a single product by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>single product by ID</returns>
        public async Task<Product> GetProduct(int Id)
        {
            return await _context.Products
                .Where(x => x.Id == Id)
                .Select(z => new Product
                {
                    Name = z.Name,
                    Description = z.Description,
                    Price = z.Price,
                    Category = z.Category,
                    URL = z.URL
                })
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// Gets the entire list of categories
        /// </summary>
        /// <returns>list of categories</returns>
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
        /// <summary>
        /// List of all the products
        /// </summary>
        /// <returns>list of products</returns>
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products
                .Select(z => new Product
                {
                    Id = z.Id,
                    Name = z.Name,
                    Description = z.Description,
                    Price = z.Price,
                    Category = z.Category,
                    URL = z.URL
                })
                .ToListAsync();
        }
        /// <summary>
        /// Adds a product to a category by their ID's
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="productId"></param>
        /// <returns>no return</returns>
        public async Task AddProductToCategory(int categoryId, int productId)
        {

            CategoryProduct categoryProduct = new CategoryProduct()
            {
                ProductId = productId,
                CategoryId = categoryId
            };
            _context.Entry(categoryProduct).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a product from a category by their ID's
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="productId"></param>
        /// <returns>no return</returns>
        public async Task RemoveProductFromCategory(int categoryId, int productId)
        {
            var categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.ProductId == productId);
            _context.Entry(categoryProduct).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates a category by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="category"></param>
        /// <returns>no return</returns>
        public async Task UpdateCategory(int Id, Category category)
        {
            Category cat = new Category()
            {
                Id = Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            _context.Entry(cat).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates a product by its ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="product"></param>
        /// <returns>no return</returns>
        public async Task UpdateProduct(int Id, Product product)
        {
            Product prod = new Product()
            {
                Id = Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                URL = product.URL
            };
            _context.Entry(prod).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes a category by its ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>no return</returns>
        public async Task DeleteCategory(int Id)
        {
            Category cat = await _context.Categories.FindAsync(Id);
            _context.Entry(cat).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes a product by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>no return</returns>
        public async Task DeleteProduct(int Id)
        {
            Product product = await _context.Products.FindAsync(Id);
            _context.Entry(product).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add a product to a cart by creating a CartProduct object
        /// </summary>
        /// <param name="cartId">  cart Id </param>
        /// <param name="productId"> product Id </param>
        /// <returns></returns>

        public async Task AddProductToCart(int cartId, int productId)
        {
            CartProducts cartProduct = new CartProducts()
            {
                ProductId = productId,
                CartId = cartId
            };
            _context.Entry(cartProduct).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a product from a users cart
        /// </summary>
        /// <param name="cartId"> cart Id </param>
        /// <param name="productId"> product Id </param>
        /// <returns></returns>
        public async Task RemoveProductFromCart(int cartId, int productId)
        {
            var cartProduct = await _context.CartProducts.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);
            _context.Entry(cartProduct).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get the user's cart object from DB
        /// </summary>
        /// <param name="Id"> cart Id </param>
        /// <returns></returns>
        public async Task<Cart> GetCart(string Id)
        {
            return await _context.Carts
                 .Where(x => x.UserId == Id)
                 .Include(x => x.CartProducts)
                 .ThenInclude(x => x.Product)
                 .Select(x => new Cart
                 {
                     Id = x.Id,
                     UserId = x.UserId,
                     CartProducts = x.CartProducts
                 }).FirstOrDefaultAsync();
        }
    }
}
