using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoEpiRepository
{
    public class GerarTokenRepository : IAuthenticationJwtServices
    {
        private readonly IConfiguration _config;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public GerarTokenRepository(IConfiguration config)
        {
            _config = config;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(User user)
        {
            var secretKey = _config["JwtConfiguration:Secret"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("Chave secreta JWT não configurada.");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.NomeCompleto}"),
                new Claim("Matricula", user.Matricula)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }
    }
}
