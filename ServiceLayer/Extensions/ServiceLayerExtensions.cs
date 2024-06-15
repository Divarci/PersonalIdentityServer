using Duende.IdentityServer;
using EntityLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using RepositoryLayer.UnitOFWorks.IdentityServer;
using ServiceLayer.Constants;
using ServiceLayer.Customizations.IdentityServer;
using ServiceLayer.Helpers.EmailSender;
using ServiceLayer.Services.AdminService;
using ServiceLayer.Services.AuthService;
using ServiceLayer.Services.IdentityServerService;
using ServiceLayer.Services.MemberService;
using System.Reflection;

namespace ServiceLayer.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 10;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredUniqueChars = 1; // 2
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false; // true
                
            })
                .AddRoleManager<RoleManager<AppRole>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //.AddErrorDescriber<LocalizationErrorDescriber>()
            //.AddPasswordValidator<CustomPasswordValidator>()
            //.AddUserValidator<CustomUserValidator>();




            var assembly = Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name;

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential()
                //.AddInMemoryIdentityResources(Config.IdentityResources())
                //.AddInMemoryApiScopes(Config.ApiScopes())
                //.AddInMemoryApiResources(Config.ApiResources())
                //.AddInMemoryClients(Config.Clients())
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>()
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = context => context.UseSqlServer(config.GetConnectionString("SqlConnection"), sqlopt => sqlopt.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = context => context.UseSqlServer(config.GetConnectionString("SqlConnection"), sqlopt => sqlopt.MigrationsAssembly(assembly));
                });





            services.AddLocalApiAuthentication();

            services.AddAuthorization(options =>
            {

                options.AddPolicy(CustomIdentityConstants.AdminRole, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(CustomIdentityConstants.AdminRole);
                });

                options.AddPolicy(CustomIdentityConstants.MemberRole, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(CustomIdentityConstants.MemberRole, CustomIdentityConstants.AdminRole);
                });
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            services.AddScoped<IIdentityServerService, IdentityServerService>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromMinutes(10);
            });



            return services;
        }
    }
}
