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
                       where usr.username == new_user.username 
                       select usr).FirstOrDefault();
            
            if (find_un != null)
                throw new CredentialsInUseException("Username is already taken");
            
            User? find_em = (from usr in _context.User
                       where usr.email == new_user.email
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
                            where usr.username == username
                            select usr).FirstOrDefault();
        
        return query_user;
    }

    public User login(string username) {
        try {
            Console.WriteLine(username);
            User? user = (from usr in _context.User
                          where usr.username == username
                          select usr).FirstOrDefault();

            if (user == null)
                throw new InvalidCredentialsException("Username or password are incorrect");
            
            return user;
        }
        catch {
            throw;  // throws exception up without altering stack trace
        }
    }
}