using UserListApp.Domain.Entities;
using UserListApp.Infrastructure.Repositories.Base;

namespace UserListApp.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<(IEnumerable<User>, int totalCount)> GetPagedAsync(string[]? queryNames, int pageNumber, int pageSize);
}
