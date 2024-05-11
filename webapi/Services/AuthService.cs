using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class AuthService {
    private readonly ZupContext _context;

    public AuthService(ZupContext context) {
        _context = context;
    }
    public string GenerateJwtToken(User user) {
        var token_handler = new JwtSecurityTokenHandler();
        string? env_key = Environment.GetEnvironmentVariable("JWT_KEY");
        if (env_key == null)
            throw new ArgumentNullException("JWT_KEY enviroment variable is not set");

        byte[] key = Encoding.ASCII.GetBytes(env_key);

        var token_descriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            }),
            Expires = DateTime.UtcNow.AddDays(3),
            Audience = "http://localhost:5000",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = token_handler.CreateToken(token_descriptor);
        return token_handler.WriteToken(token);
    }

    // public RoleLevel checkItemPermission(long user_id, long department_id, long project_id, RoleLevel level) {
    //     DepartmentService _departmentservice = new(_context);
    //     ProjectService _projectservice = new(_context);
    //     RoleService _roleservice = new(_context);

    //     if(!_departmentservice.checkIfInDepartment(user_id, department_id))
    //         return RoleLevel.NoAccess;
    //     else if (!_projectservice.checkIfInProject(user_id, project_id)) 
    //         return RoleLevel.NoAccess;
    //     else if (!_roleservice.checkItemLevel(user_id, RoleLevel.Modify))
    //         return RoleLevel.;
    //     else {
    //         // create the item
    //     }
    // }

    // public RoleLevel checkWikiPermission() {
    //     return RoleLevel.NoAccess;
    // }

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