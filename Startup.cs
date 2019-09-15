using System;
using Blazored.Modal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using INEZ.Data;
using INEZ.Data.Services;
using System.Threading.Tasks;
using INEZ.Areas.Identity;

namespace INEZ
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            if (Environment.GetEnvironmentVariable("APP_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<InezContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("InezDbConnection")));
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("InezUserDbConnection")));
            }
            else
            {
                services.AddDbContext<InezContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultInezDbConnection")));

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultInezUserDbConnection")));
            }

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddScoped<AuthenticationStateProvider, RevalidatingAuthenticationStateProvider<IdentityUser>>();
            services.AddBlazoredModal();
            services.AddScoped<CoreDataItemsService>();
            services.AddScoped<ShoppingListItemsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Automatically perform database migration
            MigrateDatabase(app);

            app.UseAuthentication();
            app.UseAuthorization();
            CreateRoles(serviceProvider).GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<InezContext>())
                {
                    context.Database.Migrate();
                }

                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // create the roles and seed them to the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //// Here you could create a super user who will maintain the web app
            //var poweruser = new ApplicationUser
            //{
            //    UserName = Configuration["AppSettings:UserName"],
            //    Email = Configuration["AppSettings:UserEmail"],
            //};
            ////Ensure you have these values in your appsettings.json file
            //string userPWD = Configuration["AppSettings:UserPassword"];

            //var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
            //if (createPowerUser.Succeeded)
            //{
            //    //here we tie the new user to the role
            //    var result = await UserManager.AddToRoleAsync(poweruser, "Admin");

            //}
        }
    }
}
