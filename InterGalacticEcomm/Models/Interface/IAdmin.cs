using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface
{
    public interface IAdmin
    {
        public Task CreateProduct(Product product);
        public Task<Product> GetProduct(int Id);
        public Task<List<Product>> GetProducts();
        public Task UpdateProduct(int Id, Product product);
        public Task DeleteProduct(int Id);

        public Task CreateCategory(Category category);
        public Task<Category> GetCategory(int Id);
        public Task AddProductToCategory(int categoryId, int productId);
        public Task RemoveProductFromCategory(int categoryId, int productId);
        public Task<List<Category>> GetCategories();
        public Task UpdateCategory(int Id, Category category);
        public Task DeleteCategory(int Id);


        public Task AddProductToCart(int cartId, int productId, int quantity);
        public Task RemoveProductFromCart(int cartId, int productId);
        public Task<Cart> GetCart(string Id);


        public Task CreateOrder(Cart cart);
        public Task<Order> GetOrder(string Id);
        public Task EmptyCart(Cart cart);

        public Task<List<Order>> GetOrders();
    }
}
