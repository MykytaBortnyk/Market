using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using MarketAPI.Models.Identity;
using MarketAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MarketAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            /*services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });*/
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.SignIn.RequireConfirmedEmail = false;
                opts.SignIn.RequireConfirmedAccount = false;
                opts.User.RequireUniqueEmail = false;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddControllers();
            services.AddMvc();
            services.AddDbContext<CatalogContext>(options => options.UseInMemoryDatabase("RamEater"));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseInMemoryDatabase("RamEater"));
            //services.AddScoped<ProductsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AppIdentityDbContext.CreateBaseAccount(app.ApplicationServices).Wait();
            CatalogContext.CreateBaseProducts(app.ApplicationServices);
        }
    }
}
