using EmailValidation;
using Microsoft.EntityFrameworkCore;
using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Interfaces;
using PonesiWebApi.Models;

namespace PonesiWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ICustomDtoMapper _mapper;
        private readonly IPasswordHasher _hasher;
        private readonly IAuthenticationService _authenticationService;

        public UserService(AppDbContext context, ICustomDtoMapper mapper,IPasswordHasher hasher,IAuthenticationService authenticationService)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
            _authenticationService = authenticationService;
        }

        public async Task<Result<string>> AuthenticateUser(UserAuthenticationDto userAuthenticationDto)
        {
            var user = await GetUserByEmailAsync(userAuthenticationDto.Email);

            if(user == null)
            {
                return Result<string>.Failure(UserErrors.NullUser);
            }

            if(!_hasher.Verify(userAuthenticationDto.Password, user.PasswordHash))
            {
                return Result<string>.Failure(UserErrors.WrongCredentials);
            }

            var jsonWebTokenResult = _authenticationService.GenerateJwtToken(user);

            return Result<string>.Success(jsonWebTokenResult.Value);

        }

        public async Task<Result> CreateUserAsync(AddNewUserDto userDto)
        {
            if (!isEmailInvalid(userDto.Email))
            {
                return Result.Failure(UserErrors.InvalidEmail);
            }
            var userExists = await CheckIfUserAlreadyExistsAsync(userDto.Username);

            if (userExists)
            {
                return Result.Failure(UserErrors.UserExists);
            }
            User newUser = _mapper.Map<AddNewUserDto, User>(userDto);

            newUser.PasswordHash = _hasher.CreateHashedPassword(userDto.Password);

            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return Result.Success();

        }

        private bool isEmailInvalid(string emailAddress)
        {
            return EmailValidator.Validate(emailAddress);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        }


        private async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        
        private async Task<bool> CheckIfUserAlreadyExistsAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email) != null;
        }

        }
    }
