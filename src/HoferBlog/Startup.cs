using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlogStorage.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogStorage;
using Microsoft.AspNetCore.Authorization;

namespace HoferBlog
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            // need this
            services.AddEntityFrameworkSqlite()
                .AddDbContext<BlogContext>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BlogContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<SmartUserManager<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/account/login";
                options.Cookies.ApplicationCookie.LogoutPath = "/account/logout";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization();

            services.AddSingleton<IAuthorizationHandler, PostAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, BlogAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }   

            app.UseStaticFiles();

            // Call UseIdentity before you call UseGoogleAuthentication. See the social authentication overview page.
            app.UseIdentity();

            // Adds a cookie-based authentication middleware to application
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = "/account/login",
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            // Plugin Google Authentication configuration options
            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = "855353314392-6sjl7u47bmi518v6vv1arf3errgs0t62.apps.googleusercontent.com",
                ClientSecret = "LyA-2bR5jmnSyTcjhKy_3frN",
                Scope = { "email", "openid" }
            });

            /*app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home";
                    await next();
                }
            });*/

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
