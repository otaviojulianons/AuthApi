using Auth.Application.Messages;
using Auth.Domain.Entities;
using System;

namespace Auth.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto ToUserResponse(this UserDomain user)
        {
            return new UserResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Permissions = user.Permissions
            };
        }

        public static UserDomain ToUserDomain(this UserRequestDto user, Guid id)
        {
            var userDomain = new UserDomain(id, user.Email, user.Password);
            foreach (var permission in user.Permissions)
                userDomain.AddPermission(permission);

            return userDomain;
        }
    }
}
