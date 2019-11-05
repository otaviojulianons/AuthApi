using Auth.Common.Cryptography;
using Auth.Common.Response;
using Auth.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Auth.Application.Services
{
    public class UserService
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IUserRepository _userRepository;

        public UserService(
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IUserRepository userRepository)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
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

                ClaimsIdentity identity = new ClaimsIdentity(
                     new GenericIdentity(user.Id.ToString(), "Login"),
                     new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
                     }
                 );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new BaseResponse<string>(token, true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().Error(ex.Message);
            }
        }

    }
}
