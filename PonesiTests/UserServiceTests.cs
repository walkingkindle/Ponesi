using Microsoft.EntityFrameworkCore;
using Moq;
using PonesiWebApi;
using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Interfaces;
using PonesiWebApi.Models;
using PonesiWebApi.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace PonesiTests
{
    public class UserServiceTests
    {
        private readonly Mock<ICustomDtoMapper> _mapper;
        private readonly Mock<IPasswordHasher> _hasher;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _mapper = new Mock<ICustomDtoMapper>();
            _hasher = new Mock<IPasswordHasher>();
            _authenticationService = new Mock<IAuthenticationService>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);

            _userService = new UserService(_context, _mapper.Object, _hasher.Object, _authenticationService.Object);
        }
         [Fact]
        public async Task AuthenticateUser_ShouldReturnResultFailure_OnEmailDoesNotExists()
        {
            string email = "nonexistingemail@email.com";

            UserAuthenticationDto dto = new UserAuthenticationDto { Email = email, Password = "somepassword" };

            var result = await _userService.AuthenticateUser(dto);

            Assert.True(result.IsFailure);

            Assert.Equal(UserErrors.NullUser, result.Error);

            
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnResultFailure_OnWrongPassword()
        {
            string mailExists = "test@mail.com";
            string correctPassword = "test";
            string someUsername = "someUsername";

            await AddMockUserToInMemoryDatabase(new User { Email = mailExists, PasswordHash = correctPassword, CreationDate = DateTime.Today, Username = someUsername, });

            UserAuthenticationDto dto = new UserAuthenticationDto { Email = mailExists, Password = "WrongPassword" };

            var result = await _userService.AuthenticateUser(dto);

            Assert.True(result.IsFailure);

            Assert.Equal(UserErrors.WrongCredentials, result.Error);


        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnResultSuccess_OnSuccess()
        {
            UserAuthenticationDto dto = new UserAuthenticationDto { Email = "test@mail.com", Password = "test" };

            await AddMockUserToInMemoryDatabase(new User { CreationDate = DateTime.Today, Email = dto.Email, PasswordHash = dto.Password, Username = "someUsername" });
            string correctPassword = "test";
            string hashedPassword = "hashed_test";
            _hasher.Setup(h => h.CreateHashedPassword(correctPassword)).Returns(hashedPassword);
    
    // Setup the hasher to return true when the correct password is verified against the hashed password
            _hasher.Setup(h => h.Verify(hashedPassword, correctPassword)).Returns(true);
    
    // Setup the hasher to return false when the wrong password is verified
            _hasher.Setup(h => h.Verify(hashedPassword, It.Is<string>(p => p != correctPassword))).Returns(false);

            var result = await _userService.AuthenticateUser(dto);

            Assert.Equal(result, Result.Success());
        }
        [Fact]
        public async Task CreateNewUser_ShouldReturnResultFailure_OnUserExists()
        {
            AddNewUserDto dto = new AddNewUserDto { Email = "test@mail.com", Username = "anyUsername", Password = "anyPassword" };

            var result = await _userService.CreateUserAsync(dto);

            Assert.Equal(result, Result.Failure(UserErrors.UserExists));
        }

        private async Task AddMockUserToInMemoryDatabase(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

        }


    }
}