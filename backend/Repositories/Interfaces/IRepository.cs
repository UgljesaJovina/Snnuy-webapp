namespace Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> Create(T obj);
    Task<ICollection<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> Update(Guid id, T obj);
    Task Delete(Guid id);
    Task SaveAsync();

    // CRUD
}