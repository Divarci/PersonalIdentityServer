
using Duende.IdentityServer.EntityFramework.DbContexts;

namespace RepositoryLayer.UnitOFWorks.IdentityServer
{
    public class UnitOfWorks : IUnitOfWorks, IDisposable
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public UnitOfWorks(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public void Dispose()
        {
            _configurationDbContext.Dispose();
        }

        public void SaveChanges()
        {
           _configurationDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _configurationDbContext.SaveChangesAsync();
        }
    }
}
