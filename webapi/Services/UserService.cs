using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class UserService {
    private readonly ZupContext _context;

    public UserService (ZupContext context) {
        _context = context;
    }

    public User getSelf(string? id_string) {
        try {
            if (id_string == null)
                throw new AccessNotAllowedException("There was an issue authenticating the claimed user's ID");

            if (!long.TryParse(id_string, out long user_id))
                throw new AccessNotAllowedException("There was an issue parsing the authenticated user's ID");

            User self = (from usr in _context.User
                        where usr.UserId == user_id
                        select usr).FirstOrDefault() ?? throw new DataNotFoundException("No user found");
            
            return self;
        }
        catch {
            throw;
        }
    }

    public User create(User new_user) {
        try {
            User? find_un = (from usr in _context.User
                       where usr.Username == new_user.Username 
                       select usr).FirstOrDefault();
            
            if (find_un != null)
                throw new CredentialsInUseException("Username is already taken");
            
            User? find_em = (from usr in _context.User
                       where usr.Email == new_user.Email
                       select usr).FirstOrDefault();
            
            if (find_em != null)
                throw new CredentialsInUseException("Email is already in use");

            _context.User.Add(new_user);
            _context.SaveChanges();

            return new_user;
        }
        catch {
            throw;
        }
    }

    public User? getByUsername(string username) {
        User? query_user = (from usr in _context.User
                            where usr.Username == username
                            select usr).FirstOrDefault();
        
        return query_user;
    }


    public string HashPassword(string plain_password) {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(plain_password);
    }

    public bool VerifyPassword(string plain_password, string hashed_password) {
        return BCrypt.Net.BCrypt.EnhancedVerify(plain_password, hashed_password);
    }

    public User login(string username, string plain_password) {
        try {
            User? user = (from usr in _context.User
                          where usr.Username == username
                          select usr).FirstOrDefault();

            if (user == null)
                throw new InvalidCredentialsException("Username or password are incorrect");
            
            if(!VerifyPassword(plain_password, user.HashedPassword))
                throw new InvalidCredentialsException("Username or password are incorrect");
            
            return user;
        }
        catch {
            throw;  // throws exception up without altering stack trace
        }
    }

    public List<MinimalDepartment> GetInvites(long user_id) {
        try {
            List<MinimalDepartment> minimalDepartments = new List<MinimalDepartment>();
            List<DepartmentInvite> invites = (from di in _context.DepartmentInvite
                                               where di.InviteeId == user_id
                                               select di).ToList();
            Console.WriteLine($"len: {invites.Count()}");
            foreach (DepartmentInvite di in invites) {
                MinimalDepartment md = new MinimalDepartment {
                    Name = (from dep in _context.Department
                            where dep.DepartmentId == di.DepartmentId
                            select dep.Name).FirstOrDefault() ?? throw new DataNotFoundException("A problem occured when fetching the invite"),
                    DepartmentId = di.DepartmentId
                };
                minimalDepartments.Add(md);
            }
            return minimalDepartments;
        }
        catch {
            throw; 
        }
    }
}