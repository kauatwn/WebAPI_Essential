using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CatalogDb.API.Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration config)
        {
            string secretKey = config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid secret key");
            byte[] privateKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKeyBytes), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                Audience = config.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = config.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(secureRandomBytes);

            string refreshToken = Convert.ToBase64String(secureRandomBytes);

            return refreshToken;
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string expiredToken, IConfiguration config)
        {
            string secretKey = config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid secret key");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
