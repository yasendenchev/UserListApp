using UserListApp.Application.DTO;

namespace UserListApp.Application.Services;

public interface IUserService
{
    Task<PagedUsersDTO> GetUsersAsync(string[]? queryNames, int? pageNumber, int? pageSize);

    Task AddAsync(UserDTO user);

    Task UpdateAsync(UserDTO user);

    Task DeleteAsync(int id);
}
