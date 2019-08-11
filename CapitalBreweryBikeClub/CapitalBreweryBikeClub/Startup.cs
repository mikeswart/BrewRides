using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CapitalBreweryBikeClub
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
            });


            services.AddDbContext<BrewRideUserDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:BrewRidesDatabaseContext"]);
            });

            services.AddDbContext<RouteDatabaseContext>(options => {
                options.UseSqlServer(Configuration["ConnectionStrings:BrewRidesDatabaseContext"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BrewRideUserDbContext>()
                .AddDefaultTokenProviders();

            services.AddRazorPages()
                .AddNewtonsoftJson();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            // services.AddAuthentication().AddGoogle(o =>
            // {
            //     // Configure your auth keys, usually stored in Config or User Secrets
            //     o.ClientId = "450721080679-auk4f14ro299n1u5gcd45h7o3rcil26d.apps.googleusercontent.com";
            //     o.ClientSecret = "bHujuAsIqajBxL30s1bHgW1-";
            //     o.Scope.Add("https://www.googleapis.com/auth/plus.login");
            //     o.ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
            //     o.SaveTokens = true;
            //     o.Events.OnCreatingTicket = ctx =>
            //     {
            //         var tokens = (List<AuthenticationToken>)ctx.Properties.GetTokens();
            //         tokens.Add(new AuthenticationToken {Name = "TicketCreated", Value = DateTime.UtcNow.ToString()});
            //         ctx.Properties.StoreTokens(tokens);
            //         return Task.CompletedTask;
            //     };
            // });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/ExternalLogin";
                options.ReturnUrlParameter = "test";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/ExternalLogin";
            });

            services.AddSingleton<RouteProvider>();
            services.AddSingleton<ScheduleProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
