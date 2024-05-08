using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using webapi.Models;

namespace webapi.Services;

public class AuthService {
    public string GenerateJwtToken(User user) {
        var token_handler = new JwtSecurityTokenHandler();
        string? env_key = Environment.GetEnvironmentVariable("JWT_KEY");
        if (env_key == null)
            throw new ArgumentNullException("JWT_KEY enviroment variable is not set");

        byte[] key = Encoding.ASCII.GetBytes(env_key);

        var token_descriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.Username),
            }),
            Expires = DateTime.UtcNow.AddDays(3),
            Audience = "http://localhost:5000",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = token_handler.CreateToken(token_descriptor);
        return token_handler.WriteToken(token);
    }


    // public ClaimsPrincipal ParseJwtToken(string token) {
    //     var token_handler = new JwtSecurityTokenHandler();
    //     string? env_key = Environment.GetEnvironmentVariable("JWT_KEY");
    //     if (env_key == null)
    //         throw new ArgumentNullException("JWT_KEY enviroment variable is not set");

    //     byte[] key = Encoding.ASCII.GetBytes(env_key);

    //     try {

    //     } 
    // }
}