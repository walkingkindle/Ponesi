using Microsoft.AspNetCore.Mvc;
using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Interfaces;
using PonesiWebApi.Models;

namespace PonesiWebApi.Controllers
{

    [Route("api/[controller]")]
    public class UserController:ControllerBase    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost()]
        public async Task<IResult> CreateNewUser(AddNewUserDto newUserDto)
        {
           var result = await _userService.CreateUserAsync(newUserDto);

            return result.Match(
             onSuccess:() => Results.NoContent(),
             onFailure: error => Results.BadRequest(result.Error));


        }
    }
}
