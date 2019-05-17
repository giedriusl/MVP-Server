using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Helpers.TokenGenerator;
using MVP.BusinessLogic.Interfaces;
using MVP.BusinessLogic.Services;
using MVP.DataAccess;
using MVP.Entities.Entities;
using MVP.Middlewares;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MVP.Helpers;

namespace MVP
{
    public class Startup
    {
        private const string AllowAllHeadersPolicy = "AllowAllHeaders";

        public Startup(IConfiguration configuration,
            IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllHeadersPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                );
            });

            services.AddDataProtection(options =>
                    options.ApplicationDiscriminator = $"{Environment.ApplicationName}")
                .SetApplicationName($"{Environment.ApplicationName}");


            services.AddMvc(options => options.Filters.Add(new RequireHttpsAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MvpContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MvpContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => { options.User.RequireUniqueEmail = true; });
            services.AddScoped<IDataSerializer<AuthenticationTicket>,
                TicketSerializer>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var validationParams = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,

                ValidateAudience = true,
                ValidAudience = Configuration["Token:Audience"],

                ValidateIssuer = true,
                ValidIssuer = Configuration["Token:Issuer"],

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:JwtKey"])),
                ValidateIssuerSigningKey = true,

                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie.Expiration = TimeSpan.FromMinutes(5);
                    options.TicketDataFormat = new JwtAuthTicketFormat(validationParams,
                        services
                            .BuildServiceProvider()
                            .GetService<IDataSerializer<AuthenticationTicket>>(),
                        services
                            .BuildServiceProvider()
                            .GetDataProtector(new[] {$"{Environment.ApplicationName}-Auth1"}));

                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = options.LoginPath;
                    options.ReturnUrlParameter = "returnUrl";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Administrator"));

                options.AddPolicy("RequireOrganizerRole",
                    policy => policy.RequireRole("Administrator", "Organizer"));

                options.AddPolicy("AllowAllRoles",
                                policy => policy.RequireRole("Administrator", "Organizer", "User"));
            });

            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOptions(AllowAllHeadersPolicy);

            app.UseHttpsRedirection();
            app.UseAuthentication();

            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
