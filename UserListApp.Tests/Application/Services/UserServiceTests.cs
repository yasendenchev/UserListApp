using Moq;
using AutoMapper;
using UserListApp.Application.DTO;
using UserListApp.Infrastructure.Repositories;
using UserListApp.Application.Services;
using UserListApp.Domain.Entities;

namespace UserListApp.Tests.Application.Services;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<IMapper> _mockMapper;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetUsersAsync_ValidParameters_ReturnsPagedUsersDTO()
    {
        var queryNames = new[] { "Matt" };
        int pageNumber = 1;
        int pageSize = 10;

        var userEntities = new List<User>
    {
        new User { Id = 1, Name = "Matt Heafy" }
    };
        var totalCount = 1;

        _mockUserRepository
            .Setup(repo => repo.GetPagedAsync(queryNames, pageNumber, pageSize))
            .ReturnsAsync((userEntities, totalCount));

        _mockMapper
            .Setup(m => m.Map<IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
            .Returns(new List<UserDTO>
            {
            new UserDTO { Id = 1, Name = "Matt Heafy" }
            });

        var result = await _userService.GetUsersAsync(queryNames, pageNumber, pageSize);

        Assert.IsNotNull(result);
        Assert.AreEqual(totalCount, result.TotalCount);
        Assert.AreEqual(1, result.Users.Count());
        Assert.AreEqual("Matt Heafy", result.Users.First().Name);
    }

    [Test]
    public async Task GetUsersAsync_NullPageNumber_SetsDefaultPageNumber()
    {
        int defaultPageSize = 0;

        var queryNames = new[] { "Matt" };
        int pageNumber = 0;
        int? pageSize = null;

        var userEntities = new List<User>
    {
        new User { Id = 1, Name = "Matt Heafy" }
    };
        var totalCount = 1;

        _mockUserRepository
            .Setup(repo => repo.GetPagedAsync(queryNames, pageNumber, defaultPageSize))
            .ReturnsAsync((userEntities, totalCount));

        _mockMapper
            .Setup(m => m.Map<IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
            .Returns(new List<UserDTO>
            {
            new UserDTO { Id = 1, Name = "Matt Heafy" }
            });

        var result = await _userService.GetUsersAsync(queryNames, pageNumber, pageSize);

        Assert.IsNotNull(result);
        Assert.AreEqual(totalCount, result.TotalCount);
        Assert.AreEqual(1, result.Users.Count());
        Assert.AreEqual("Matt Heafy", result.Users.First().Name);
    }

    [Test]
    public async Task GetUsersAsync_NullPageSize_SetsDefaultPageSize()
    {
        int defaultPageNumber = 0;

        var queryNames = new[] { "Matt" };
        int? pageNumber = null;
        int pageSize = 10;

        var userEntities = new List<User>
    {
        new User { Id = 1, Name = "Matt Heafy" }
    };
        var totalCount = 1;

        _mockUserRepository
            .Setup(repo => repo.GetPagedAsync(queryNames, defaultPageNumber, pageSize))
            .ReturnsAsync((userEntities, totalCount));

        _mockMapper
            .Setup(m => m.Map<IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
            .Returns(new List<UserDTO>
            {
            new UserDTO { Id = 1, Name = "Matt Heafy" }
            });

        var result = await _userService.GetUsersAsync(queryNames, pageNumber, pageSize);

        Assert.IsNotNull(result);
        Assert.AreEqual(totalCount, result.TotalCount);
        Assert.AreEqual(1, result.Users.Count());
        Assert.AreEqual("Matt Heafy", result.Users.First().Name);
    }

    [Test]
    public async Task AddAsync_ValidUser_CallsRepositoryAddAsync()
    {
        var userDto = new UserDTO { Id = 1, Name = "James Hetfield" };
        var userEntity = new User { Id = 1, Name = "James Hetfield" };

        _mockMapper
            .Setup(m => m.Map<User>(It.IsAny<UserDTO>()))
            .Returns(userEntity);

        await _userService.AddAsync(userDto);

        _mockUserRepository.Verify(repo => repo.AddAsync(userEntity), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_ValidUser_CallsRepositoryUpdateAsync()
    {
        var userDto = new UserDTO { Id = 1, Name = "Alejandro Garnacho" };
        var userEntity = new User { Id = 1, Name = "Alejandro Garnacho" };

        _mockMapper
            .Setup(m => m.Map<User>(It.IsAny<UserDTO>()))
            .Returns(userEntity);

        await _userService.UpdateAsync(userDto);

        _mockUserRepository.Verify(repo => repo.UpdateAsync(userEntity), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ValidId_CallsRepositoryDeleteAsync()
    {
        var userId = 1;

        await _userService.DeleteAsync(userId);

        _mockUserRepository.Verify(repo => repo.DeleteAsync(userId), Times.Once);
    }
}