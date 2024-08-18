
using Microsoft.EntityFrameworkCore;
using UserListApp.Infrastructure.Persistance;

namespace UserListApp.Infrastructure.Repositories.Base;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly UserListAppContext context;
    protected readonly DbSet<T> dbSet;

    public Repository(UserListAppContext _context)
    {
        context = _context;
        dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await context.SaveChangesAsync();
    }
}
