using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Data
{
    public class GalacticDbContext : IdentityDbContext<AppUser>
    {
        public GalacticDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryProduct>().HasKey(x => new { x.CategoryId, x.ProductId });
            modelBuilder.Entity<CartProducts>().HasKey(x => new { x.CartId, x.ProductId });

            SeedRole(modelBuilder, "Admin", "read", "create", "update", "delete");
            SeedRole(modelBuilder, "Guest", "read");

            modelBuilder.Entity<Product>().HasData(
                new Product { Name = "Plumbus", Description = "The plumbus is...well...a plumbus", Price = 199.99m, Id = 1 },
                new Product { Name = "Pickle Rick", Description = "Pickles but with faces", Price = 24.99m, Id = 2 },
                new Product { Name = "Morty", Description = "Just another Morty", Price = 99.99m, Id = 3 },
                new Product { Name = "Pencil Vester", Description = "Practical pencil....but suspicious", Price = 29.99m, Id = 4 },
                new Product { Name = "Ghost-In-A-Jar", Description = "Rahhhhh dude", Price = 50.00m, Id = 5 },
                new Product { Name = "PlumbusMachine", Description = "Creates new and cool plumbus!", Price = 499.99m, Id = 6 },
                new Product { Name = "Cucumber Rick", Description = "Cucumber but with faces...like the pickle but cheaper", Price = 14.99m, Id = 7 },
                new Product { Name = "Space Machine", Description = "FlyingSpaceCraft4.0", Price = 999.99m, Id = 8 },
                new Product { Name = "Mr. Meeseeks Box", Description = "He will help you achieve that which you need.", Price = 99.99m, Id = 9 },
                new Product { Name = "Jerry", Description = "please take them all...They are literally free", Price = 1.99m, Id = 10 },
                new Product { Name = "Cool Shirts", Description = "It's literally just a shirt, but cool", Price = 29.99m, Id = 11 },
                new Product { Name = "Pants", Description = "The pants we have are literally better", Price = 30.99m, Id = 12 },
                new Product { Name = "Quick Save Box", Description = "We replace you, with a temporary version of you, there is no returns", Price = 850.99m, Id = 13 },
                new Product { Name = "Level", Description = "We ensure 100% level surfaces with this bad boy", Price = 480.29m, Id = 14 },
                new Product { Name = "Portal Gun", Description = "Use with care.", Price = 451.99m, Id = 15 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryName = "Expensive", Description = "This costs a LOT of flurbos", Id = 1 },
                new Category { CategoryName = "Morty-Tier", Description = "Literally costs chocolate", Id = 2 },
                new Category { CategoryName = "InterDimensional Cable", Description = "As seen in T.V", Id = 3 },
                new Category { CategoryName = "Cheap", Description = "You know why you're here", Id = 4 },
                new Category { CategoryName = "Testing 1", Description = "literally a fake category", Id = 5 },
                new Category { CategoryName = "Testing 2", Description = "fake stuff for filling the page", Id = 6 }
                );
            modelBuilder.Entity<CategoryProduct>().HasData(
                new CategoryProduct
                {
                    CategoryId = 1,
                    ProductId = 1
                },
                new CategoryProduct
                {
                    CategoryId = 2,
                    ProductId = 2
                },
                new CategoryProduct
                {
                    CategoryId = 3,
                    ProductId = 3
                });

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProducts> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

        private int id = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permissions.Select(permission =>
               new IdentityRoleClaim<string>
               {
                   Id = id++,
                   RoleId = role.Id,
                   ClaimType = "permissions",
                   ClaimValue = permission
               });
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }


}
