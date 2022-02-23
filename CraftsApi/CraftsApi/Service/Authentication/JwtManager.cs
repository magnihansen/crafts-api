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

        public JwtManager(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _key = _configuration["Jwt:SecurityKey"].ToString();
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("fullName", user.Firstname + " " + user.Lastname),
                new Claim("id", user.Id.ToString()),
                new Claim("userData", JsonConvert.SerializeObject(user)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:ValidAudience"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:ValidIssuer"])
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
