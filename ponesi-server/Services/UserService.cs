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

        public UserService(AppDbContext context, ICustomDtoMapper mapper,IPasswordHasher hasher)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task<Result> AuthenticateUser(UserAuthenticationDto userAuthenticationDto)
        {
            var user = await GetUserByEmailAsync(userAuthenticationDto.Email);

            if(user == null)
            {
                return Result.Failure(UserErrors.NullUser);
            }

            if(!_hasher.Verify(userAuthenticationDto.Password, user.PasswordHash))
            {
                return Result.Failure(UserErrors.WrongCredentials);
            }

            //jwt????
            return Result.Success();

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
