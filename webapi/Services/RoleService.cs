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

    public Role GetById(long role_id) {
        try {
            Role role = (from rl in _context.Role
                        where rl.RoleId == role_id
                        select rl).FirstOrDefault() ?? throw new DataNotFoundException("The selected role could not be found");
            return role;
        }
        catch {
            throw;
        }
    }

    public Role? GetByUserId(long user_id, long project_id) {
        try {
            Console.WriteLine($"rid: {project_id}");
            Role? role = (from pur in _context.ProjectUserRole
                          where pur.UserId == user_id && pur.ProjectId == project_id
                          select pur.Role).FirstOrDefault();
            Console.WriteLine($"role: {role}");
            return role;
        }
        catch {
            throw;
        }
    }

    public void Delete(long role_id) {
        try {
            Role role = GetById(role_id);
            _context.Remove(role);
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }

    public bool checkItemLevel(long user_id, RoleLevel level) {
        try {
            bool meetsLevel = false;
            Role? role = (from pur in _context.ProjectUserRole
                         where pur.UserId == user_id
                         select pur.Role).FirstOrDefault();
            
            if (role == null) 
                meetsLevel = false;
            else if (level >= role.ItemLevel)
                meetsLevel = true;
            else
                meetsLevel = false;

            return meetsLevel;
        }
        catch {
            throw;
        }
    }

    public bool checkWikiLevel(long user_id, RoleLevel level) {
        try {
            bool meetsLevel = false;
            Role? role = (from pur in _context.ProjectUserRole
                         where pur.UserId == user_id
                         select pur.Role).FirstOrDefault();
            
            if (role == null) 
                meetsLevel = false;
            else if (level >= role.WikiLevel)
                meetsLevel = true;
            else
                meetsLevel = false;

            return meetsLevel;
        }
        catch {
            throw;
        }
    }
}