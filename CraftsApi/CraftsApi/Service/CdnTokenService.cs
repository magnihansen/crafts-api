using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CraftsApi.Auth;
using CraftsApi.DomainModels;
using CraftsApi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CraftsApi.Service
{
    public class CdnTokenService : ICdnTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IDomainRepository _domainRepository;
        private readonly ICdnTokenRepository _cdnTokenRepository;

        private const string SecurityKey = "Jwt:SecurityKey";
        private const string ValidAudience = "Jwt:ValidAudience";
        private const string ValidIssuer = "Jwt:ValidIssuer";

        public CdnTokenService(
            IConfiguration configuration,
            IDomainRepository domainRepository,
            ICdnTokenRepository cdnTokenRepository)
        {
            _configuration = configuration;
            _domainRepository = domainRepository;
            _cdnTokenRepository = cdnTokenRepository;
        }

        public async Task<string> GenerateCdnTokenAsync(string host)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecurityKey]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("DomainId", JsonConvert.SerializeObject(domain.Id)),
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
    }
}

