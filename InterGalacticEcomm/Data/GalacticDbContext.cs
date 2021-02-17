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

            //seedRole stuff can go here.
            SeedRole(modelBuilder, "Admin", "read", "create", "update", "delete");
            SeedRole(modelBuilder, "Guest", "read");

            modelBuilder.Entity<Product>().HasData(
                new Product {Name = "Plumbus", Description = "The plumbus is...well...a plumbus", Price = 199.99m, Id = 1},
                new Product { Name = "Pickle Rick", Description = "Pickles but with faces", Price = 24.99m, Id = 2 },
                new Product { Name = "Morty", Description = "Just another Morty", Price = 99.99m, Id = 3 },
                new Product { Name = "Pencil Vester", Description = "Practical pencil....but suspicious", Price = 29.99m, Id = 4 },
                new Product { Name = "Ghost-In-A-Jar", Description = "Rahhhhh dude", Price = 50.00m, Id = 5 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryName = "Expensive", Description="This costs a LOT of flurbos", Id = 1},
                new Category { CategoryName = "Morty-Tier", Description = "Literally costs chocolate", Id = 2 },
                new Category { CategoryName = "InterDimensional Cable", Description = "As seen in T.V", Id = 3 }
                );


            string ADMIN_ID = "34234gety45tb45v45";
            string ROLE_ID = "admin_permission";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = ROLE_ID,
                    ConcurrencyStamp = ROLE_ID
                });


            //create user
            var appUser = new AppUser
            {
                UserName = "SuperAdmin",
                Id = ADMIN_ID
            };

            //set user password
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Password!23");

            //seed user
            modelBuilder.Entity<AppUser>().HasData(appUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });


            /*
            RegisterUser newAdmin = new RegisterUser
            {
                UserName = "Admin1",
                Password = "Password!23",
                Email = "admin@admin.com",
                Roles = new List<string>() { "Admin" }
            };

            

            modelBuilder.Entity<AppUser>().HasData(
                
                );
            */

            /*
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { UserName = "username" };
            userManager.Create(user, "password");
            */
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }


        //put the seed role stuff here
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
            //claim type comes from startup seed method call, line 90ish
        }
    }


}
