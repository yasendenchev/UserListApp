using UserListApp.Application.DTO;

namespace UserListApp.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO> GetByIdAsync(int id);
    Task AddAsync(UserDTO user);
    Task UpdateAsync(UserDTO user);
    Task DeleteAsync(int id);
    Task<IEnumerable<UserDTO>> GetByNamesAsync(IEnumerable<string> names);
    Task<IEnumerable<UserDTO>> GetPageAsync(int pageNumber, int pageSize);
}
