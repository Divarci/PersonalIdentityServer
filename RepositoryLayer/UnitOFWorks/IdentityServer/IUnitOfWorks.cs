namespace RepositoryLayer.UnitOFWorks.IdentityServer
{
    public interface IUnitOfWorks
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
