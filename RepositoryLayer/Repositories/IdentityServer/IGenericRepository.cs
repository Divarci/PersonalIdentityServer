namespace RepositoryLayer.Repositories.IdentityServer
{
    public interface IGenericRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
