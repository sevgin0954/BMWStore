using AutoMapper;
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
using BMWStore.Services.Interfaces;
using BMWStore.Services;
using System.IO;
using BMWStore.Web.Mapping;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BMWStore.Common.Constants;
using System.Runtime.CompilerServices;
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
            services.AddDefaultIdentity<User>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequireUppercase = false,
                    RequiredUniqueChars = 3,
                    RequireNonAlphanumeric = false
                };
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            RegisterServiceLayer(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            var assembly = Assembly.GetAssembly(typeof(SeedDbService));
            var allClassesTypes = this.GetTypesFromAssembly(assembly, type => type.IsClass);
            this.AddScopedServices(services, allClassesTypes);
        }

        private IEnumerable<Type> GetTypesFromAssembly(Assembly assembly, Func<Type, bool> func)
        {
            var classesTypes = new List<Type>();

            var servicesTypes = assembly.GetTypes();
            foreach (var type in servicesTypes)
            {
                if (IsCompilerGenerated(type) == false && func(type))
                {
                    classesTypes.Add(type);
                }
            }

            return classesTypes;
        }

        private bool IsCompilerGenerated(Type type)
        {
            return type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        private void AddScopedServices(IServiceCollection services, IEnumerable<Type> allClassesTypes)
        {
            foreach (var classType in allClassesTypes)
            {
                var interfaceTypes = classType.GetInterfaces();

                if (interfaceTypes.Length != 1)
                {
                    throw new Exception(ErrorConstants.IncorrectInterfacesCount);
                }

                var firstInterfaceType = interfaceTypes[0];
                services.AddScoped(firstInterfaceType, classType);
            }
        }
    }
}
