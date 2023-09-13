using Microsoft.EntityFrameworkCore;
using Repositories.DAL;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DataContext ctx;
    protected readonly DbSet<T> table;

    public Repository(DataContext _ctx) {
        ctx = _ctx;
        table = ctx.Set<T>();
    }

    public virtual async Task<T> Create(T obj)
    {
        await table.AddAsync(obj);
        await SaveAsync();
        return obj;
    }

    public virtual async Task<ICollection<T>> GetAll()
    {
        return await table.ToListAsync();
    }

    public virtual async Task<ICollection<T>> GetAll(Func<T, bool> predicate) {
        return (await table.ToListAsync()).Where(predicate).ToList();
    }

    public virtual async Task<T> GetById(Guid id)
    {
        T? instance = await table.FindAsync(id);

        if (instance is null) throw new KeyNotFoundException();

        return instance;
    }

    public virtual async Task<T> Update(Guid id, T obj)
    {
        T? instance = await table.FindAsync(id);

        if (instance is null) throw new KeyNotFoundException();

        instance = obj;
        await SaveAsync();
        return  instance;
    }

    public virtual async Task Delete(Guid id)
    {
        T? instance = await table.FindAsync(id);

        if (instance is null) throw new KeyNotFoundException();

        table.Remove(instance);
        await SaveAsync();
    }


    public async Task SaveAsync()
    {
        await ctx.SaveChangesAsync();
    }
}