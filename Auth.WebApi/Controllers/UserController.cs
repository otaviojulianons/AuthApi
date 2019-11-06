using Auth.Application.Messages;
using Auth.Application.Services;
using Auth.Common.Response;
using Auth.Domain.Repositories;
using Auth.Infrastructure.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Auth.WebApi.Controllers
{
    [Route("users")]
    public class UserController : BaseController
    {
        private UserService _userService;

        public UserController(
            UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public BaseResponse<IEnumerable<UserResponseDto>> GetUsers()
        {
            return _userService.GetAll();
        }

        [HttpGet("{id}")]
        public BaseResponse<UserResponseDto> GetById([FromRoute]Guid id)
        {
            return _userService.GetById(id);
        }

        //[Authorize(Roles = AuthRoleNames.Admin)]
        [HttpPost]
        public BaseResponse CreateUser([FromBody] UserCreateDto createUserRequest)
        {
            return _userService.CreateUser(createUserRequest);
        }

        //[Authorize(Roles = AuthRoleNames.Admin)]
        [HttpDelete("{id}")]
        public BaseResponse DeleteUser([FromRoute]Guid id)
        {
            return _userService.DeleteUser(id);
        }
    }
}
