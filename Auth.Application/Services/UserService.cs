using Auth.Common.Response;
using Auth.Domain.Repositories;

namespace Auth.Application.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseResponse<string> Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                return new BaseResponse<string>().Error("User not found.");

            if(user.Password != password)
                return new BaseResponse<string>().Error("Invalid User/Password.");


            return new BaseResponse<string>("");
        }

    }
}
