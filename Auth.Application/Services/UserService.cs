using Auth.Application.Mappers;
using Auth.Application.Messages;
using Auth.Common.Cryptography;
using Auth.Common.Response;
using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Auth.Infrastructure.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auth.Application.Services
{
    public class UserService
    {
        private JwtTokenService _jwtTokenService;
        private IUserRepository _userRepository;

        public UserService(
            JwtTokenService jwtTokenService,
            IUserRepository userRepository)
        {
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        public BaseResponse<string> Login(string email, string password)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user == null)
                    return new BaseResponse<string>().Error("User not found.");

                var passwordEncrypt = CryptographyService.GetMd5Hash(password);
                if (user.Password != passwordEncrypt)
                    return new BaseResponse<string>().Error("Invalid User/Password.");

                var token = _jwtTokenService.GererateToken(user);
                return new BaseResponse<string>(token, true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().Error(ex.Message);
            }
        }

        public BaseResponse<UserResponseDto> GetById(Guid id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
                return new BaseResponse<UserResponseDto>(user.ToUserResponse());
            else
                return new BaseResponse<UserResponseDto>().Error("User not found.");
        }

        public BaseResponse<IEnumerable<UserResponseDto>> GetAll()
        {
            var users = _userRepository.GetAll();
            var usersResponse = users.Select(user => user.ToUserResponse());

            return new BaseResponse<IEnumerable<UserResponseDto>>(usersResponse);
        }

        public BaseResponse CreateUser(UserRequestDto createUserRequest)
        {
            try
            {
                var userEmail = _userRepository.GetUserByEmail(createUserRequest.Email);
                if (userEmail != null)
                    return new BaseResponse().Error("Email already exists.");

                var user = createUserRequest.ToUserDomain(Guid.NewGuid());

                var result = _userRepository.Insert(user);
                return new BaseResponse(result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().Error(ex.Message);
            }
        }

        public BaseResponse UpdateUser(Guid id, UserRequestDto updateUserRequest)
        {
            try
            {
                var userEmail = _userRepository.GetUserById(id);
                if (userEmail == null)
                    return new BaseResponse().Error("User not found.");

                var userDomain = updateUserRequest.ToUserDomain(id);

                var result = _userRepository.Update(userDomain);
                return new BaseResponse(result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().Error(ex.Message);
            }
        }

        public BaseResponse DeleteUser(Guid id)
        {
            try
            {
                var result = _userRepository.Delete(id);
                return new BaseResponse(result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().Error(ex.Message);
            }
        }
    }
}
