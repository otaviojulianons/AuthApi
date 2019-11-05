using Auth.Application.Messages;
using Auth.Application.Services;
using Auth.Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost()]
        public BaseResponse<string> Login([FromBody] UserAuthenticateRequest userAuthenticate)
        {
            var response = _userService.Login(userAuthenticate.Email, userAuthenticate.Password);
            return FormatResponse(response);
        }

        [Authorize]
        [HttpGet]
        public string Teste()
        {
            return "OK";
        }

        private BaseResponse<T> FormatResponse<T>(BaseResponse<T> response)
        {
            Response.StatusCode = response.Success ? 200 : 400;
            return response;
        }
    }
}
