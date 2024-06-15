using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Repositories.IdentityServer
{
    public class GenericRepositoy<T> : IGenericRepository<T> where T : class
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoy(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
            _dbSet = _configurationDbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }               

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
