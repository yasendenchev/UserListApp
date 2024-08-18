using AutoMapper;
using UserListApp.Application.DTO;
using UserListApp.Domain.Entities;
using UserListApp.Infrastructure.Repositories;

namespace UserListApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserService(IUserRepository _userRepository, IMapper _mapper)
    {
        userRepository = _userRepository;
        mapper = _mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var userEntities = await this.userRepository.GetAllAsync();
        var users = this.mapper.Map<IEnumerable<UserDTO>>(userEntities);

        return users;
    }

    public async Task<UserDTO> GetByIdAsync(int id)
    {
        var entity = await this.userRepository.GetByIdAsync(id);
        var user = this.mapper.Map<UserDTO>(entity);

        return user;
    }

    public async Task AddAsync(UserDTO user)
    {
        var entity = this.mapper.Map<User>(user);
        await this.userRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(UserDTO user)
    {
        var entity = this.mapper.Map<User>(user);
        await this.userRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await this.userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserDTO>> GetByNamesAsync(IEnumerable<string> names)
    {
        if (names == null || names.Any() == false)
        {
            return await GetAllAsync();
        }

        var lowerCaseNames = names.Select(x => x.Trim().ToLower()).ToHashSet();

        var userEntities = await this.userRepository.GetByNamesAsync(lowerCaseNames);
        var filteredUsers = this.mapper.Map<IEnumerable<UserDTO>>(userEntities);

        return filteredUsers;
    }

    public async Task<IEnumerable<UserDTO>> GetPageAsync(int pageNumber, int pageSize)
    {
        var userEntities = await this.userRepository.GetPageAsync(pageNumber, pageSize);
        var result = this.mapper.Map<IEnumerable<UserDTO>>(userEntities);

        return result;
    }
}
