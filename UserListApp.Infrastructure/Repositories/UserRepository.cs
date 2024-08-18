
using Microsoft.EntityFrameworkCore;
using UserListApp.Domain.Entities;
using UserListApp.Infrastructure.Persistance;
using UserListApp.Infrastructure.Repositories.Base;

namespace UserListApp.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(UserListAppContext context) : base(context) { }

    public async Task<IEnumerable<User>> GetPageAsync(int pageNumber, int pageSize)
    {
        var data = await dbSet
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return data;
    }

    public async Task<IEnumerable<User>> GetByNamesAsync(IEnumerable<string> names)
    {
        var data = await dbSet
            .Where(x => names.Contains(x.Name.ToLower()))
            .ToListAsync();

        return data;
    }
}
