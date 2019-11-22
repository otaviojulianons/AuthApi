using Auth.Application.Messages;
using Auth.Application.Services;
using Auth.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebApi.Controllers
{
    [Route("authentication")]
    public class LoginController : BaseController
    {
        private UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public BaseResponse<string> Login([FromBody] UserAuthenticateDto userAuthenticate)
        {
            var response = _userService.Login(userAuthenticate.Email, userAuthenticate.Password);
            return FormatResponse(response);
        }
    }
}