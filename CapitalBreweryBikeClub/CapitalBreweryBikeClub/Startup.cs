using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddRazorPages(options =>
                {
                    //options.Conventions.AddPageRoute("/Schedule", "");
                })
                .AddNewtonsoftJson();

            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddUserStore<TestUserStore>().AddRoleStore<TestUserStore>();

            services.AddAuthentication().AddGoogle(o =>
            {
                // Configure your auth keys, usually stored in Config or User Secrets
                //o.ClientId = con;
                //o.ClientSecret = "<yoursecret>";
                //o.Scope.Add("https://www.googleapis.com/auth/plus.login");
                //o.ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
                //o.SaveTokens = true;
                //o.Events.OnCreatingTicket = ctx =>
                //{
                //    var tokens = (List<AuthenticationToken>) ctx.Properties.GetTokens();
                //    tokens.Add(new AuthenticationToken {Name = "TicketCreated", Value = DateTime.UtcNow.ToString()});
                //    ctx.Properties.StoreTokens(tokens);
                //    return Task.CompletedTask;
                //};
                o.SaveTokens = true;
            });

            services.AddSingleton<RouteProvider>();
            services.AddSingleton<ScheduleProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private sealed class TestUserStore: IUserStore<ApplicationUser>, IRoleStore<IdentityRole>
        {
            public void Dispose()
            {
            }

            public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            Task<IdentityRole> IRoleStore<IdentityRole>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            Task<IdentityRole> IRoleStore<IdentityRole>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
