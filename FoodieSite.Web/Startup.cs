
using FoodieSite.BLL;
using FoodieSite.BOL;
using FoodieSite.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web
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
            services.AddControllersWithViews();

            // Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1440);
            });

            #region BLL Services
            services.AddTransient<IRestaurantMasterBs, RestaurantMasterBs>();
            services.AddTransient<ICategoryMasterBs, CategoryMasterBs>();
            services.AddTransient<IItemMasterBs, ItemMasterBs>();
            services.AddTransient<IImageMasterBs, ImageMasterBs>();
            services.AddTransient<IUserBs, UserBs>();
            #endregion

            #region DAL Services
            services.AddTransient<IRestaurantMasterDb, RestaurantMasterDb>();
            services.AddTransient<ICategoryMasterDb, CategoryMasterDb>();
            services.AddTransient<IItemMasterDb, ItemMasterDb>();
            services.AddTransient<IImageMasterDb, ImageMasterDb>();
            services.AddTransient<IUserDb, UserDb>();
            #endregion

            //Connection String
            services.AddDbContext<FoodieSiteDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppConfig")));
            services.AddIdentity<AppUsers, IdentityRole>()
                    .AddEntityFrameworkStores<FoodieSiteDbContext>()
                    .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Security/Login");

            // Authorize Filter
            //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //services.AddControllersWithViews(x => x.Filters.Add(new AuthorizeFilter(policy)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "defualt",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
