using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Models;

namespace PonesiWebApi.Interfaces
{
    public interface IAuthenticationService
    {
        Result<string> GenerateJwtToken(User user);
    }
}
