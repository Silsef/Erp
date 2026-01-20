namespace Erp_Api.Models.Repository.Interfaces
{
    public interface SearchableRepository<T, TKey>
    {
        Task<T?> GetByNameAsync(TKey key);
    }
}
