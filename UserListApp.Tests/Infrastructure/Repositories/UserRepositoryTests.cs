using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserListApp.Domain.Entities;
using UserListApp.Infrastructure.Persistance;
using UserListApp.Infrastructure.Repositories;

namespace UserListApp.Tests.Repositories;

[TestFixture]
public class UserRepositoryTests
{
    private Mock<DbSet<User>> _mockDbSet;
    private Mock<UserListAppContext> _mockContext;
    private UserRepository _userRepository;

    [SetUp]
    public void SetUp()
    {
        _mockDbSet = new Mock<DbSet<User>>();
        _mockContext = new Mock<UserListAppContext>();
        _mockContext.Setup(c => c.Set<User>()).Returns(_mockDbSet.Object);

        _userRepository = new UserRepository(_mockContext.Object);
    }

    [Test]
    public async Task GetPagedAsync_ShouldReturnPagedResults()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "Alice" },
            new User { Id = 2, Name = "Bob" },
            new User { Id = 3, Name = "Charlie" },
            new User { Id = 4, Name = "David" }
        }.AsQueryable();

        _mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        // Act
        var (result, totalCount) = await _userRepository.GetPagedAsync(null, 1, 2);

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual(4, totalCount);
        Assert.IsTrue(result.Any(user => user.Name == "Charlie"));
        Assert.IsTrue(result.Any(user => user.Name == "David"));
    }

    [Test]
    public async Task GetPagedAsync_WithFilter_ShouldReturnFilteredResults()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "Alice" },
            new User { Id = 2, Name = "Bob" },
            new User { Id = 3, Name = "Charlie" },
            new User { Id = 4, Name = "David" }
        }.AsQueryable();

        _mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        // Act
        var (result, totalCount) = await _userRepository.GetPagedAsync(new[] { "a" }, 0, 2);

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual(4, totalCount);
        Assert.IsTrue(result.Any(user => user.Name == "Alice"));
        Assert.IsTrue(result.Any(user => user.Name == "Charlie"));
    }

    [Test]
    public async Task GetPagedAsync_EmptyResult_ShouldReturnEmptyResults()
    {
        // Arrange
        var users = new List<User>().AsQueryable();

        _mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
        _mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        // Act
        var (result, totalCount) = await _userRepository.GetPagedAsync(null, 0, 10);

        // Assert
        Assert.AreEqual(0, result.Count());
        Assert.AreEqual(0, totalCount);
    }
}
