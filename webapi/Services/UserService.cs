using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class UserService {
    private readonly ZupContext _context;

    public UserService (ZupContext context) {
        _context = context;
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
            Console.WriteLine(username);
            User? user = (from usr in _context.User
                          where usr.Username == username
                          select usr).FirstOrDefault();

            if(user == null) { Console.WriteLine("null"); }

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
}