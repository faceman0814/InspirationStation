namespace EntityFramework.Repository;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
