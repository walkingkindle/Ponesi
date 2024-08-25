using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Models;

namespace PonesiWebApi.Interfaces
{
    public interface IUserService
    {
        Task<Result> CreateUserAsync(AddNewUserDto newUserDto);

        Task<User> GetUserByIdAsync(int userId);

        Task<Result<string>> AuthenticateUser(UserAuthenticationDto userAuthenticationDto);
    }
}
