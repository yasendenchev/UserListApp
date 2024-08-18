
using Microsoft.EntityFrameworkCore;
using UserListApp.Domain.Entities;
using UserListApp.Infrastructure.Persistance;
using UserListApp.Infrastructure.Repositories.Base;

namespace UserListApp.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{

    public UserRepository(UserListAppContext context) : base(context) { }

    public async Task<(IEnumerable<User>, int totalCount)> GetPagedAsync(string[]? queryNames, int pageNumber, int pageSize)
    {
        IQueryable<User> data = dbSet.Select(x => x).AsQueryable<User>();
        var totalCount = await data.CountAsync();

        if (queryNames != null && queryNames.Length > 0)
        {
            data = data
                .Where(user => queryNames
                    .Any(name => user.Name
                        .ToLower()
                        .Contains(name)
                ));
        }
        
        data = data
            .Skip(pageNumber * pageSize)
            .Take(pageSize);

        var users = await data.ToListAsync();

        return (users, totalCount);
    }
}
