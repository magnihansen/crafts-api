using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CraftsApi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CraftsApi.Service.Authentication
{
    public class JwtManager : IJwtManager
    {
        private readonly string _key;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IDomainRepository _domainRepository;

        private const string SecurityKey = "Jwt:SecurityKey";
        private const string ValidAudience = "Jwt:ValidAudience";
        private const string ValidIssuer = "Jwt:ValidIssuer";

        public JwtManager(IUserService userService, IConfiguration configuration, IDomainRepository domainRepository)
        {
            _userService = userService;
            _configuration = configuration;
            _domainRepository = domainRepository;
            _key = _configuration[SecurityKey].ToString();
        }

        public async Task<string> Authenticate(string username, string password)
        {
            ViewModels.UserVM user = await _userService.GetUserByCredientialsAsync(username, password);
            if (user == null)
            {
                return null;
            }
            return GenerateToken(user);
        }

        public async Task<ViewModels.UserVM> GetUserByIdentity(ClaimsIdentity identity)
        {
            string userData = identity.Claims.Where(p => p.Type == "userData").FirstOrDefault()?.Value;
            ViewModels.UserVM user = JsonConvert.DeserializeObject<ViewModels.UserVM>(userData);
            return await Task.Run(() => user);
        }

        private string GenerateToken(ViewModels.UserVM user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecurityKey]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("fullName", user.Firstname + " " + user.Lastname),
                new Claim("id", user.Id.ToString()),
                new Claim("email", JsonConvert.SerializeObject(user.Email)),
                new Claim("isAdmin", JsonConvert.SerializeObject(user.IsAdmin)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration[ValidAudience]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration[ValidIssuer])
            };

            var token = new JwtSecurityToken(
                issuer: _configuration[ValidIssuer],
                audience: _configuration[ValidAudience],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateImageCdnToken(string host)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecurityKey]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "ApiUser"),
                new Claim("domainId", JsonConvert.SerializeObject(domain.Id)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration[ValidAudience]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration[ValidIssuer])
            };

            var token = new JwtSecurityToken(
                issuer: _configuration[ValidIssuer],
                audience: _configuration[ValidAudience],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Tuple<bool, string> ValidateCurrentToken(string token)
        {
            byte[] signingKey = Encoding.UTF8.GetBytes(_configuration[SecurityKey]);
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
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey)
                }, out SecurityToken validatedToken);
            }
            catch (SecurityTokenInvalidIssuerException stiie)
            {
                return new Tuple<bool, string>(false, stiie.Message);
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
