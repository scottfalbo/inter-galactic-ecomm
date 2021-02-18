using InterGalacticEcomm.Data;
using InterGalacticEcomm.Models;
using InterGalacticEcomm.Models.Interface;
using InterGalacticEcomm.Models.Interface.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GalacticDbContext>(options =>{
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<GalacticDbContext>();

            services.AddScoped<JwtTokenService>();

            services.AddAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("read", policy => policy.RequireClaim("permissions", "read"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddTransient<IUserService, IdentityUserService>();
            services.AddTransient<IAdmin, AdminRepository>();

            services.AddMvc();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
