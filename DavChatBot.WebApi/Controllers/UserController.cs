using DatChatBot.Services.DTOs;
using DavChatBot.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace DavChatBot.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            var success = await _userService.SignIn(signInDTO);
            return new OkObjectResult(success);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            var response = await _userService.SignUp(signUpDTO);
            return new OkObjectResult(response);
        }
    }
}

