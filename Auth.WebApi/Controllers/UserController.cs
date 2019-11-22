using Auth.Application.Messages;
using Auth.Application.Services;
using Auth.Common.Response;
using Auth.Domain.Entities;
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

        [Authorize(UserPolicies.Admin)]
        [HttpGet]
        public BaseResponse<IEnumerable<UserResponseDto>> GetUsers()
        {
            return _userService.GetAll();
        }

        [Authorize(UserPolicies.Admin)]
        [HttpGet("{id}")]
        public BaseResponse<UserResponseDto> GetById([FromRoute]Guid id)
        {
            return _userService.GetById(id);
        }

        [Authorize(UserPolicies.Admin)]
        [HttpPost]
        public BaseResponse CreateUser([FromBody] UserRequestDto createUserRequest)
        {
            return _userService.CreateUser(createUserRequest);
        }

        [Authorize(UserPolicies.Admin)]
        [HttpPut("{id}")]
        public BaseResponse UpdateUser([FromRoute]Guid id, [FromBody] UserRequestDto updateUserRequest)
        {
            return _userService.UpdateUser(id, updateUserRequest);
        }

        [Authorize(UserPolicies.Admin)]
        [HttpDelete("{id}")]
        public BaseResponse DeleteUser([FromRoute]Guid id)
        {
            return _userService.DeleteUser(id);
        }
    }
}
