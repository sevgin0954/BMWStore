using Microsoft.AspNetCore.Builder;
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
using BMWStore.Data.Interfaces;
using ServiceLayerRegistrar;
using MappingRegistrar;
using System.Reflection;
using BMWStore.Models;
using CloudinaryDotNet;
using BMWStore.Data.Repositories.Generic.Interfaces;
using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories;
using BMWStore.Data.Repositories.Interfaces;

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
            // TODO: ADD ANTI FORGERY FILTER
            RegisterServiceLayer(services);

            var cloudinaryAccount = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);
            services.AddCloudinary(cloudinaryAccount, ServiceLifetime.Singleton);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RegisterAutoMapperMappings.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(User).GetTypeInfo().Assembly);

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

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
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
            services.AddScoped<IBMWStoreUnitOfWork, BMWStoreUnitOfWork>();

            var serviceRegistrar = new ServiceCollectionRegistrar(services);
            serviceRegistrar.AddScopedServices(typeof(SeedDbService));
            serviceRegistrar.AddScopedServices(typeof(CarRepository));
        }
    }
}