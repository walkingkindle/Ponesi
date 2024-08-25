using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Interfaces;
using PonesiWebApi.Models;

namespace PonesiWebApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class UserController:ControllerBase    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IResult> CreateNewUser(AddNewUserDto newUserDto)
        {
           var result = await _userService.CreateUserAsync(newUserDto);

            return result.Match(
             onSuccess:() => Results.NoContent(),
             onFailure: error => Results.BadRequest(result.Error));


        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IResult> LoginUser(UserAuthenticationDto userAuthDto)
        {
            var result = await _userService.AuthenticateUser(userAuthDto);

            return result.Match(
                onSuccess: () => Results.Ok(result.Value),
                onFailure: error => Results.BadRequest(result.Error));
        }

        [HttpGet("id")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if(user == null)
            {
                return BadRequest(UserErrors.NullUser);
            }

            return Ok(user);
        }
    }
}
