using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using Repository.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject;


public class UserUnitTest
{
    [Fact]
    public async Task TestLogin_Successful()
    {
        // Arrange
        var mockUserContext = new Mock<Product326075108Context>();
        var users = new List<User>
        {
            new User { Email = "testuser@gmail.com", Password = "password" }
        };

        mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

        var userRepository = new UserRepository(mockUserContext.Object);

        // Act
        var user = await userRepository.Login("testuser@gmail.com", "password");

        // Assert
        Assert.NotNull(user);
        Assert.Equal("testuser@gmail.com", user.Email);
    }

    [Fact]
    public async Task TestLogin_Failed()
    {
        // Arrange
        var mockUserContext = new Mock<Product326075108Context>();
        var users = new List<User>
    {
        new User { Email = "testuser", Password = "password" }
    };

        mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

        var userService = new UserRepository(mockUserContext.Object);

        // Act
        var user = await userService.Login( "testuser", "wrongpassword" );

        // Assert
        Assert.Null(user);
    }
    [Fact]
    public async Task TestRegister_Successful()
    {
        // Arrange
        var mockUserContext = new Mock<Product326075108Context>();
        var users = new List<User>();

        mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

        var userRepository = new UserRepository(mockUserContext.Object);
        var newUser = new User { Email = "newuser@gmail.com", Password = "newpassword" };

        // Act
        var registeredUser = await userRepository.Register(newUser);

        // Assert
        Assert.NotNull(registeredUser);
        Assert.Equal("newuser@gmail.com", registeredUser.Email);
        mockUserContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        mockUserContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task TestRegister_DuplicateEmail()
    {
        // Arrange
        var mockUserContext = new Mock<Product326075108Context>();
        var existingUser = new List<User>
            {
                new User { Email = "existinguser@gmail.com", Password = "password" }
            };

        mockUserContext.Setup(x => x.Users).ReturnsDbSet(existingUser);

        var userRepository = new UserRepository(mockUserContext.Object);
        var newUser = new User { Email = "existinguser@gmail.com", Password = "newpassword" };

        // Act
        var registeredUser = await userRepository.Register(newUser);

        // Assert
        Assert.Contains(existingUser, u => u.Email == "existinguser@gmail.com");
        Assert.Null(registeredUser);
        mockUserContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        mockUserContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }


    //[Fact]
    //public async Task TestRegister_MissingEmail()
    //{
    //    // Arrange
    //    var mockUserContext = new Mock<Product326075108Context>();
    //    var users = new List<User>();

    //    mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

    //    var userRepository = new UserRepository(mockUserContext.Object);
    //    var newUser = new User { Password = "newpassword" };

    //    // Act & Assert
    //    await Assert.ThrowsAsync<ArgumentException>(async () => await userRepository.Register(newUser));

    //    mockUserContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    //    mockUserContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    //}

    //[Fact]
    //public async Task TestRegister_MissingPassword()
    //{
    //    // Arrange
    //    var mockUserContext = new Mock<Product326075108Context>();
    //    var users = new List<User>();

    //    mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

    //    var userRepository = new UserRepository(mockUserContext.Object);
    //    var newUser = new User { Email = "newuser@gmail.com" };

    //    // Act & Assert
    //    await Assert.ThrowsAsync<ArgumentException>(async () => await userRepository.Register(newUser));

    //    mockUserContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    //    mockUserContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    //}

    [Fact]
    public async Task TestRegister_DbFailure()
    {
        // Arrange
        var mockUserContext = new Mock<Product326075108Context>();
        var users = new List<User>();

        mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);
        mockUserContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Database error"));

        var userRepository = new UserRepository(mockUserContext.Object);
        var newUser = new User { Email = "newuser@gmail.com", Password = "newpassword" };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await userRepository.Register(newUser));

        mockUserContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        mockUserContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

}

