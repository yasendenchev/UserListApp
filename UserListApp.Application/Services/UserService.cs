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

    public async Task<PagedUsersDTO> GetUsersAsync(string[]? queryNames, int? pageNumber, int? pageSize)
    {
        if (pageNumber.HasValue && (!pageSize.HasValue || pageSize < 1))
        {
            pageSize = 10;
        }

        if (pageSize.HasValue && (!pageNumber.HasValue || pageNumber < 0))
        {
            pageNumber = 0;
        }

        var (userEntities, totalCount) = await this.userRepository.GetPagedAsync(queryNames, pageNumber.Value, pageSize.Value);
        var usersDto = this.mapper.Map<IEnumerable<UserDTO>>(userEntities);

        var pagedUsersDto = new PagedUsersDTO
        {
            Users = usersDto,
            TotalCount = totalCount
        };

        return pagedUsersDto;
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
}
