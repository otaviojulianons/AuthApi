using Auth.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Auth.Infrastructure.Jwt
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
           

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);

             var signingConfigurations = new SigningConfigurations(tokenConfigurations);
            services.AddSingleton(signingConfigurations);

            services.AddSingleton(tokenConfigurations);
            services.AddSingleton<JwtTokenService>();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingConfigurations.Key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                options.DefaultPolicy = policy.Build();

                var policyAdmin = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policyAdmin.RequireAuthenticatedUser();
                policyAdmin.RequireClaim(ClaimTypes.Role, UserRoles.Admin);
                options.AddPolicy(UserPolicies.Admin, policyAdmin.Build());
            });

        }
    }
}
