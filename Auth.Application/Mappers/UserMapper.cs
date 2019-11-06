using Auth.Application.Messages;
using Auth.Domain.Entities;

namespace Auth.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto ToUserResponse(this UserDomain user)
        {
            return new UserResponseDto()
            {
                Email = user.Email,
                Password = user.Password,
                Permissions = user.Permissions
            };
        }
    }
}
