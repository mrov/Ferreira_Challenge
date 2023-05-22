using AutoMapper;
using Bogus;
using Ferreira_Challenge.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using Models.DTOs.Input;
using Moq;
using Utils.Enums;

public class UserRepositoryTests
{
    private DbContextOptions<MyDbContext> _dbContextOptions;
    private readonly Mock<IConfiguration> _configuration;
    private IMapper _mapper;

    public UserRepositoryTests()
    {
        // Set up an in-memory database for testing
        _dbContextOptions = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "FREE")
            .Options;

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public async Task GetUserById_Should_Return_User_When_Exists()
    {
        // Arrange

        var user = generateUser();

        using (var context = new MyDbContext(_dbContextOptions))
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        using (var context = new MyDbContext(_dbContextOptions))
        {
            var repository = new UserRepository(context, _mapper);

            // Act
            var result = await repository.GetUserById(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }
    }

    [Fact]
    public async Task GetUserByUsernameAsync_Should_Return_User_When_Exists()
    {
        // Arrange
        var user = generateUser();
        using (var context = new MyDbContext(_dbContextOptions))
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        using (var context = new MyDbContext(_dbContextOptions))
        {
            var repository = new UserRepository(context, _mapper);

            // Act
            var result = await repository.GetUserByUsernameAsync(user.Login);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Login, result.Login);
        }
    }

    [Fact]
    public async Task GetFilteredUsers_Should_Return_Paginated_Users()
    {
        // Arrange
        var user = generateUser();
        var filter = new UserFilterDTO { PageNumber = 1 };
        using (var context = new MyDbContext(_dbContextOptions))
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        using (var context = new MyDbContext(_dbContextOptions))
        {
            var repository = new UserRepository(context, _mapper);

            // Act
            var result = await repository.GetFilteredUsers(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Users.Count);
        }
    }

    private User generateUser ()
    {
        var faker = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Login, f => f.Person.UserName)
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.CPF, f => f.Random.Replace("###.###.###-##"))
            .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
            .RuleFor(u => u.MotherName, f => f.Person.FullName)
            .RuleFor(u => u.Status, f => f.PickRandom<Status>())
            .RuleFor(u => u.InsertedAt, f => f.Date.Past())
            .RuleFor(u => u.UpdatedAt, f => f.Date.Recent()); ;

        var user = faker.Generate();

        return user;
    }
}