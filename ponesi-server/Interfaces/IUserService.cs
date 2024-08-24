using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Models;

namespace PonesiWebApi.Interfaces
{
    public interface IUserService
    {
        Task<Result> CreateUserAsync(AddNewUserDto newUserDto);
    }
}
