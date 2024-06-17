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
using System.Reflection;
using ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiScopeServices;
using ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceServices;
using ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceScopeServices;
using ServiceLayer.Services.IdentityServerService.ClientServices;
using ServiceLayer.Services.MemberServices;
using ServiceLayer.Services.AdminServices;
using ServiceLayer.Services.AuthServices;
using ServiceLayer.Services.IdentityServerService.ClientServices.ClientGrantTypeServices;
using ServiceLayer.Services.IdentityServerService.ClientServices.ClientScopeServices;
using ServiceLayer.Services.IdentityServerService.ClientServices.ClientSecretServices;

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
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IApiScopeService, ApiScopeService>();
            services.AddScoped<IApiResourceService, ApiResourceService>();
            services.AddScoped<IApiResourceScopeService, ApiResourceScopeService>();
            services.AddScoped<IClientGrantTypeService, ClientGrantTypeService>();
            services.AddScoped<IClientScopeService, ClientScopeService>();
            services.AddScoped<IClientSecretService, ClientSecretService>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromMinutes(10);
            });



            return services;
        }
    }
}
