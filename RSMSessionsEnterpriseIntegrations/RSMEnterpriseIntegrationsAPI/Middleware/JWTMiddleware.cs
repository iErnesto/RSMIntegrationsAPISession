using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
using RSMEnterpriseIntegrationsAPI.Domain.Models;

namespace RSMEnterpriseIntegrationsAPI.Middleware
{
    public class JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings, IUserLoginRepository repository)
    {
        private readonly RequestDelegate _next = next;
        private readonly IUserLoginRepository _userLoginRepository = repository;

        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                await AttachUserToContext(context, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

             
                var userLogin = await _userLoginRepository.GetUserLogin(userId);

                context.Items["User"] = userLogin;

            }
            catch
            {
                throw new BadRequestException("Invalid Credentials");
            }
        }
    }
}
