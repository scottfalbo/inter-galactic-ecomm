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
                new Product { Name = "Ghost-In-A-Jar", Description = "Rahhhhh dude", Price = 50.00m, Id = 5 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryName = "Expensive", Description = "This costs a LOT of flurbos", Id = 1 },
                new Category { CategoryName = "Morty-Tier", Description = "Literally costs chocolate", Id = 2 },
                new Category { CategoryName = "InterDimensional Cable", Description = "As seen in T.V", Id = 3 }
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
