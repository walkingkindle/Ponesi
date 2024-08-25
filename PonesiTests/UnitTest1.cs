using Moq;
using PonesiWebApi;
using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Interfaces;
using PonesiWebApi.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace PonesiTests
{
    public class UserServiceTests
    {
        private readonly Mock<AppDbContext> _context;
        private readonly Mock<ICustomDtoMapper> _mapper;
        private readonly Mock<IPasswordHasher> _hasher;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly IUserService _userService;

        public UserServiceTests(IUserService userService)
        {
            _context = new Mock<AppDbContext>();
            _mapper = new Mock<ICustomDtoMapper>();
            _hasher = new Mock<IPasswordHasher>();
            _authenticationService = new Mock<IAuthenticationService>();
            _userService = userService;     
        }
        [Fact]
        public async Task AuthenticateUser_ShouldReturnResultFailure_OnMissingEmail()
        {
            string email = "nonexistingemail@email.com";

            UserAuthenticationDto dto = new UserAuthenticationDto { Email = email, Password = "somepassword" };

            var result = await _userService.AuthenticateUser(dto);

            Assert.Equal(result,Result.Failure(UserErrors.NullUser));
            
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnResultFailure_OnWrongPassword()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnResultFailure_TokenGenerationFailure()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task AuthenticateUser_ShouldReturnResultSuccess_OnSuccess()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task CreateNewUser_ShouldReturnResultFailure_OnUserExists()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task CreateNewUser_ShouldReturnResultFailure_OnDatabaseSaveFail()
        {
            throw new NotImplementedException();
        }

    }
}