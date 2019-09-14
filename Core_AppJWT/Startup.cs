using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core_AppJWT.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core_AppJWT.Models;
using Core_AppJWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.OAuth;


namespace Core_AppJWT
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<MyAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataAppContextConnection"));
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // register the authenticaiton service
            services.AddTransient<AuthenticationService>();

            services.AddDefaultIdentity<IdentityUser>()
                 .AddDefaultUI(UIFramework.Bootstrap4)
                 .AddEntityFrameworkStores<ApplicationDbContext>();


            //services.AddIdentity<IdentityUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();




            // add CORS policy 
            services.AddCors(options => options.AddPolicy("corspolicy", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            // add the Authentication Service with the defaults for bearer Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.ClaimsIssuer = "webapi";
                // 1. Load the JWT Secret Key to Verify and Validate Token
                // read key from appsettings.json
                var secretKey = Convert.FromBase64String(Configuration["JWTSettings:SecretKey"]);
                // 2. Defining the Mechanism for Validating Received Token from Client
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiUser", policy => policy.RequireClaim("username"));
            //});

            services.AddScoped<IRepository<Product, int>, ProductRepository>();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver
              = new DefaultContractResolver())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseCors("corspolicy");
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
