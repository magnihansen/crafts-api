using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CraftsApi.Service.Authentication
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;
        private readonly string _key;
        private readonly IUserService _userService;

        private const string SecurityKey = "Jwt:SecurityKey";
        private const string ValidAudience = "Jwt:ValidAudience";
        private const string ValidIssuer = "Jwt:ValidIssuer";

        public JwtManager(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _key = _configuration[SecurityKey].ToString();
        }

        public async Task<string> Authenticate(string username, string password)
        {
            ViewModels.User user = await _userService.GetUserByCredientialsAsync(username, password);
            if (user == null)
            {
                return null;
            }
            return GenerateToken(user);
        }

        public async Task<ViewModels.User> GetUserByIdentity(ClaimsIdentity identity)
        {
            string userData = identity.Claims.Where(p => p.Type == "userData").FirstOrDefault()?.Value;
            ViewModels.User user = JsonConvert.DeserializeObject<ViewModels.User>(userData);
            return await Task.Run(() => user);
        }

        private string GenerateToken(ViewModels.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecurityKey]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("fullName", user.Firstname + " " + user.Lastname),
                new Claim("id", user.Id.ToString()),
                new Claim("userData", JsonConvert.SerializeObject(user)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration[ValidAudience]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration[ValidIssuer])
            };

            var token = new JwtSecurityToken(
                issuer: _configuration[ValidIssuer],
                audience: _configuration[ValidAudience],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1), //.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Tuple<bool, string> ValidateCurrentToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecurityKey]));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration[ValidIssuer],
                    ValidAudience = _configuration[ValidAudience],
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch (SecurityTokenExpiredException stee)
            {
                return new Tuple<bool, string>(false, stee.Message);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }

            return new Tuple<bool, string>(true, "Success");
        }

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
    }
}
