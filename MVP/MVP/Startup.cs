using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
using MVP.EmailService;
using MVP.EmailService.Interfaces;

namespace MVP
{
    public class Startup
    {
        private readonly string ALLOW_ALL_HEADERS_POLICY = "AllowAllHeaders";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(ALLOW_ALL_HEADERS_POLICY,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                );
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MvpContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MvpContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => { options.User.RequireUniqueEmail = true; });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
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
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailManager, EmailManager>();
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

            app.UseOptions(ALLOW_ALL_HEADERS_POLICY);

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
