using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSS.Data;
using OSS.Models;
using OSS.Services;

namespace OSS
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddDbContext<SurveySystemDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("SurveySystemDbConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            /**/
            CreateRoles(serviceProvider).Wait();
            /**/
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }




        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //if(!UserManager.Users.Any())
            //{
            //    var poweruser = new ApplicationUser
            //    {
            //        UserName = "Admin",
            //        Email = "admin@email.com",
            //    };
            //    string adminPassword = "12345678";

            //     UserManager.CreateAsync(poweruser, adminPassword);
            //}

            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin","EP","EI","MIM","MEV","KIMB","FIN","FSF"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            //var poweruser = new ApplicationUser
            //{

            //    UserName = Configuration["AppSettings:AdminUserEmail"],
            //    Email = Configuration["AppSettings:AdminUserEmail"],
            //};
            foreach (var role in roleNames) {
                var user = new ApplicationUser
                {

                    UserName = Configuration["AppSettings:" + role + "UserEmail"],
                    Email = Configuration["AppSettings:" + role + "UserEmail"],
                };
                var userPWD = Configuration["AppSettings:" + role + "UserPassword"];

                var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:" + role + "UserEmail"]);

                if (_user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(user, userPWD);
                    if (createPowerUser.Succeeded)
                    {
                        //here we tie the new user to the role
                        await UserManager.AddToRoleAsync(user, role);

                    }
                }
            }

            //Ensure you have these values in your appsettings.json file
            //string userPWD = Configuration["AppSettings:AdminUserPassword"];
            //var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

            //if (_user == null)
            //{
            //    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
            //    if (createPowerUser.Succeeded)
            //    {
            //        //here we tie the new user to the role
            //        await UserManager.AddToRoleAsync(poweruser, "Admin");

            //    }
            //}



        }
    }
}
