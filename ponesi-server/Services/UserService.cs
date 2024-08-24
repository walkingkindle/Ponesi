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

        public UserService(AppDbContext context, ICustomDtoMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> CreateUserAsync(AddNewUserDto userDto)
        {
            var userExists = await CheckIfUserAlreadyExistsAsync(userDto.Username);

            if (userExists)
            {
                return Result.Failure(UserErrors.UserExists);
            }
            User newUser = _mapper.Map<AddNewUserDto, User>(userDto);

            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return Result.Success();

        }
        
        private async Task<bool> CheckIfUserAlreadyExistsAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username) != null;
        }

        }
    }
