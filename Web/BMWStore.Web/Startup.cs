﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services;
using ServiceLayerRegistrar;
using MappingRegistrar;
using System.Reflection;
using BMWStore.Models;
using CloudinaryDotNet;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.Interfaces;
using BMWStore.Web.Filters;
using BMWStore.Services.Models;
using BMWStore.Web.Middlewares;

namespace BMWStore.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequireUppercase = false,
                    RequiredUniqueChars = 3,
                    RequireNonAlphanumeric = false
                };
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAntiforgery();

            RegisterServiceLayer(services);

            services.AddSession();
            services.AddDistributedMemoryCache();

            var cloudinaryAccount = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);
            services.AddCloudinary(cloudinaryAccount, ServiceLifetime.Singleton);

            services.AddMvc(options => 
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new ModelStateActionFilter());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RegisterAutoMapperMappings.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(User).GetTypeInfo().Assembly,
                typeof(CarServiceModel).Assembly);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseResponseXFrame();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "paginationIndex",
                    template: "{area:exists}/{controller}/{action=Index}/{filter?}/{name?}/{pageNumber?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        private void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();

            var serviceRegistrar = new ServiceCollectionRegistrar(services);
            serviceRegistrar.AddIgnored(typeof(CacheService));
            serviceRegistrar.AddScopedServices(typeof(CarModelTypeService));
            serviceRegistrar.AddScopedServices(typeof(CarRepository));
            serviceRegistrar.AddScopedServices(typeof(AdminCarsService));
        }
    }
}