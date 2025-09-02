using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace fullstackBackend.WebApi.App.Services
{
   public class TokenService
   {
      private readonly string _secret;

      public TokenService(IConfiguration configuration)
      {
         _secret = configuration["Jwt:Secret"];
      }

      public string GenerateToken(string userId)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(_secret);
         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(new Claim[]
             {
                new Claim(ClaimTypes.NameIdentifier, userId)
             }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };
         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }
   }
}
