using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherAPP.Application.Services.Authentication;
using WeatherAPP.Infrastructure.Context;
using WeatherAPP.Infrastructure.Mediating;

namespace WeatherAPP.API.Controllers.Authentication
{
    public class AuthController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly IUserPrincipal principal;

        public AuthController(IServiceMediator mediator, IConfiguration configuration, IUserPrincipal principal) : base(mediator)
        {
            this.configuration = configuration;
            this.principal = principal;
        }

        #region Login & Register
        [HttpPost("auth/loginToken")]
        public async Task<IActionResult> LoginToken([FromBody] LoginRequest request)
        {
            var claims = await PrepareClaims(request);
            var token = PrepareToken(claims);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            return ResponseHandler(token);
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await mediator.Do<RegisterRequest, RegisterResponse>(request);

            return ResponseHandler(response);
        }

        [Authorize]
        [HttpGet("auth/logoutToken")]
        public IActionResult LogoutToken()
        {
            return ResponseHandler(new
            {
                token = string.Empty,
                success = true
            });
        }
        #endregion

        #region Private methods
        private async Task<List<Claim>> PrepareClaims(LoginRequest request)
        {
            var response = await mediator.Do<LoginRequest, LoginResponse>(request);

            if (!response.Success)
            {
                return new List<Claim>();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.User.Id.ToString()),
                new Claim(ClaimTypes.Email, response.User.Email.ToString()),
                new Claim(ClaimTypes.Name, response.User.FirstName.ToString()),
                new Claim(ClaimTypes.Surname, response.User.LastName.ToString()),
                new Claim(ClaimTypes.GivenName, $"{response.User.FirstName} {response.User.LastName}"),
            };

            return claims;
        }

        private string PrepareToken(List<Claim> claims)
        {
            if (!claims.Any())
            {
                return string.Empty;
            }

            var issuer = configuration["OAuth:Issuer"];
            var audience = configuration["OAuth:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["OAuth:Key"]);

            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
        #endregion
    }
}
