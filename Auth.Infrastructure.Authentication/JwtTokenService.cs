﻿using Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Auth.Infrastructure.Jwt
{
    public class JwtTokenService
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public JwtTokenService(
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public string GererateToken(UserDomain user)
        {
            var identity = UserToClaimsIdentity(user);
            DateTime expires = DateTime.Now + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var securityToken = new JwtSecurityToken
            (
                issuer: null,
                audience: null,
                signingCredentials: _signingConfigurations.SigningCredentials,
                expires: expires,
                claims: identity.Claims
            );
            
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private ClaimsIdentity UserToClaimsIdentity(UserDomain user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            if (user.Permissions.Any())
                claims.AddRange(user.Permissions.Select(permission => new Claim(ClaimTypes.Role, permission)));

            return new ClaimsIdentity(new ClaimsIdentity(claims), claims);
        }
    }
}
