using UserListApp.Domain.Entities;
using UserListApp.Infrastructure.Repositories.Base;

namespace UserListApp.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetPageAsync(int pageNumber, int pageSize);

    Task<IEnumerable<User>> GetByNamesAsync(IEnumerable<string> names);
}
