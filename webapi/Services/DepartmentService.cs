using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class DepartmentService {
    private readonly ZupContext _context;

    public DepartmentService(ZupContext context) {
        _context = context;
    }

    public Department create(Department department) {
        try {
            _context.Department.Add(department);
            _context.SaveChanges();

            return department;
        } catch {
            throw;
        }
    }

    public Department getById(long department_id) {
        try {
            Department? department = (from dep in _context.Department
                                      where dep.DepartmentId == department_id
                                      select dep).FirstOrDefault();
            
            if (department == null)
                throw new DataNotFoundException("The selected department could not be found");
            else
                return department;
        } catch {
            throw;
        }
    }

    public List<Department> getByUser(long user_id) {
        try {
            long[] department_ids = (from dm in _context.DepartmentMember
                                     where dm.MemberId == user_id
                                     select dm.DepartmentId).ToArray();

            List<Department> departments = (from dep in _context.Department
                                            where department_ids.Contains(dep.DepartmentId)
                                            select dep).ToList();
            
            return departments;
        }
        catch {
            throw;
        }
    }

    public void inviteUser(long department_id, long invitee_id) {
        try {
            Department? department = (from dep in _context.Department
                                      where dep.DepartmentId == department_id
                                      select dep).FirstOrDefault() ?? throw new DataNotFoundException("The selected department could not be found");
            
            User? invitee = (from usr in _context.User
                             where usr.UserId == invitee_id
                             select usr).FirstOrDefault() ?? throw new DataNotFoundException("The selected user could not be found");

            DepartmentInvite invite = new DepartmentInvite {
                DepartmentId = department.DepartmentId,
                InviteeId = invitee.UserId,
                Response = InviteResponse.Pending
            };

            _context.DepartmentInvite.Add(invite);
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }

    public User getOwner(long department_id) {
        try {
            User? owner = (from dep in _context.Department
                           join usr in _context.User on dep.OwnerId equals usr.UserId
                           where dep.DepartmentId == department_id
                           select usr).FirstOrDefault() ?? throw new DataNotFoundException("No owner was found or the selected department could not be found");

            return owner;
        }
        catch {
            throw;
        }
    }

    public void AddUserToDepartment(long user_id, long department_id) {
        var departmentMember = new DepartmentMember {
            MemberId = user_id,
            DepartmentId = department_id
        };

        _context.DepartmentMember.Add(departmentMember);
        _context.SaveChanges();
    }

    public bool checkIfInDepartment(long user_id, long department_id) {
        try {

            bool is_department_member = (from dm in _context.DepartmentMember
                                         where dm.MemberId == user_id && dm.DepartmentId == department_id
                                         select dm).Any();
            
            return is_department_member;
        }
        catch {
            throw;
        }
    }

    public List<Project> GetProjects(long department_id) {
        try {
            List<Project> projects = (from prj in _context.Project
                                      where prj.DepartmentId == department_id
                                      select prj).ToList();
            
            return projects;                 
        }
        catch {
            throw;
        }
    }

    public List<Role> GetRoles(long department_id) {
        try {
            List<Role> roles = (from rl in _context.Role
                                where rl.DepartmentId == department_id
                                select rl).ToList();
            return roles;
        }
        catch {
            throw;
        }
    }
}
