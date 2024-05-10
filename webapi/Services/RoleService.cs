using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class RoleService {
    private readonly ZupContext _context;

    public RoleService(ZupContext context) {
        _context = context;
    }

    public Role Create(Role new_role) {
        try {
            bool role_exists = (from rl in _context.Role
                                where rl.Name == new_role.Name
                                select rl).Any();
            
            if (role_exists)
                throw new ObjectNameInUseException("A role with the provided name already exists within the department");

            _context.Add(new_role);
            _context.SaveChanges();

            return new_role;
        }
        catch {
            throw;
        }
    }
}