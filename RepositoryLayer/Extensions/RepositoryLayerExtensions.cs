using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using RepositoryLayer.Repositories.IdentityServer;

namespace RepositoryLayer.Extensions
{
    public static class RepositoryLayerExtensions
    {
        public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services,IConfiguration config)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("SqlConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepositoy<>));
            
            return services;
        }
    }
}
