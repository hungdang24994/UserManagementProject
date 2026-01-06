using Moq;
using UserManagementApi.Models;
using UserManagementApi.Repositories;
using UserManagementApi.Services;
using Xunit;

namespace UserManagementApi.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void GetById_UserExists_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Name = "Hung",
                Status = "Active"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetById(userId))
                .Returns(user);

            // Act
            var result = _userService.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public void GetById_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepositoryMock
                .Setup(repo => repo.GetById(userId))
                .Returns((User?)null);

            // Act
            var result = _userService.GetById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAll_WithStatus_FiltersCorrectly()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Name = "A", Status = "Active" },
                new User { Name = "B", Status = "Inactive" }
            };

            _userRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(users);

            // Act
            var result = _userService.GetAll("Active");

            // Assert
            Assert.Single(result);
            Assert.Equal("Active", result.First().Status);
        }


        [Fact]
        public void Create_ValidUser_CallsAddAndReturnsUser()
        {
            // Arrange
            var user = new User
            {
                Name = "Hung",
                Email = "hung@example.com",
                Status = "Active"
            };

            // Act
            var result = _userService.Create(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public void UpdateStatus_UserExists_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Status = "Inactive" };

            _userRepositoryMock
                .Setup(repo => repo.GetById(userId))
                .Returns(user);

            // Act
            var result = _userService.UpdateStatus(userId, "Active");

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.Update(user), Times.Once);
        }

    }
}
